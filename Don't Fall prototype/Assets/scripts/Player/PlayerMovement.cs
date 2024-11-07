using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float xRotateSpeed = 10f;
    public float yRotateSpeed = 10f;
    public Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded;
    public bool wasGrounded;
    private Rigidbody rb;
    private float yrValue;
    private float xrValue;
    private Camera _mainCamera;
    private AudioSource[] audioSources;

    // Indices for specific audio sources
    private int groundAudioIndex = 0; // Audio for landing
    private int moveAudioIndex = 1; // Audio for walking/moving

    // Raycast settings
    public float groundRayLength = 1.1f; // Length of the ray used for ground detection
    public LayerMask groundLayer; // The layer mask to detect ground objects
    public Color rayColor = Color.red; // Color for visualizing the ray in the editor

    // Dust particle system
    public ParticleSystem dustParticleSystem; // Reference to the particle system
    private ParticleSystem.EmissionModule dustEmission; // To control emission of dust particles

    // Jumping variables for double jump
    private int jumpCount = 0; // Track number of jumps performed
    public int maxJumpCount = 2; // Max number of jumps (1 for double jump)

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        _mainCamera = Camera.main;
        audioSources = GetComponents<AudioSource>();

        // Get the EmissionModule from the dust particle system
        if (dustParticleSystem != null)
        {
            dustEmission = dustParticleSystem.emission;
            dustEmission.enabled = false; // Start with the dust system disabled
        }
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();

        // Handle jumping: Check if the player is grounded or has room for a double jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || jumpCount < maxJumpCount) // Allow jump if grounded or double jumping
            {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                jumpCount++; // Increment the jump count

                // If the player is grounded, reset jump count
                if (isGrounded)
                {
                    jumpCount = 1; // Set to 1 because the first jump is still a valid jump
                }

                isGrounded = false; // Player is no longer grounded
            }
        }

        // Handle landing audio (only play once when the player lands)
        if (isGrounded && !wasGrounded)
        {
            audioSources[groundAudioIndex].Play(); // Play landing sound
            Debug.Log("Landing audio played.");
            TriggerDustEffect(); // Trigger dust effect
        }

        wasGrounded = isGrounded; // Update grounded state for the next frame
    }

    private void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 cameraForward = Vector3.Scale(_mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = (cameraForward * moveVertical + _mainCamera.transform.right * -moveHorizontal).normalized;

        if (movement.magnitude > 0)
        {
            rb.AddForce(movement * moveSpeed);

            // Play walking audio only if it's not already playing
            if (!audioSources[moveAudioIndex].isPlaying)
            {
                audioSources[moveAudioIndex].Play();
                Debug.Log("Walking audio started.");
            }
        }
        else
        {
            if (audioSources[moveAudioIndex].isPlaying)
            {
                audioSources[moveAudioIndex].Stop();
                Debug.Log("Walking audio stopped.");
            }
        }
    }

    private void RotatePlayer()
    {
        xrValue = Input.GetAxis("Mouse X") * xRotateSpeed * Time.deltaTime;
        yrValue = Input.GetAxis("Mouse Y") * yRotateSpeed * Time.deltaTime;

        transform.Rotate(0f, xrValue, 0f);
    }

    // Use Raycast to check if the player is grounded
    private void FixedUpdate()
    {
        RaycastHit hit;

        // Raycast downwards to check if the player is grounded
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundRayLength, groundLayer))
        {
            if (!isGrounded) // Only set to grounded if it's the first time landing
            {
                isGrounded = true;
                jumpCount = 0; // Reset jump count when the player touches the ground
                Debug.Log("Ground detected by raycast.");
            }
        }
        else
        {
            if (isGrounded) // If the player is in the air, update isGrounded to false
            {
                isGrounded = false;
                Debug.Log("Player is in the air, not grounded.");
            }
        }

        // Debugging: Draw the ray in the scene view to visualize ground detection
        Debug.DrawRay(transform.position, Vector3.down * groundRayLength, rayColor);
    }

    // Trigger the dust particle effect when the player lands
    private void TriggerDustEffect()
    {
        // Ensure that dust particles are only triggered when the player actually lands
        if (dustParticleSystem != null && isGrounded)
        {
            // Trigger the dust effect only when grounded
            dustEmission.enabled = true;
            dustParticleSystem.Play();
            Debug.Log("Dust particle system triggered.");
        }
    }
}
