using System;
using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Build;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private float originalMoveSpeed;
    private Vector3 v3Velocity;
    public float xRotateSpeed = 200f;
    public float yRotateSpeed = 100f;
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
    private int groundAudioIndex = 0; 
    private int moveAudioIndex = 1; 

    // Dust particle system
    public ParticleSystem dustParticleSystem; // Reference to the particle system
    private ParticleSystem.EmissionModule dustEmission; // To control emission of dust particles

    // Jumping variables for double jump
    private int jumpCount = 0; // Track number of jumps performed
    public int maxJumpCount = 2; // Max number of jumps (1 for double jump)
    
    public int catapultXValue = 10;
    public int catapultYValue = 10;

    private void Start()
    {
        originalMoveSpeed = moveSpeed;
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
            //wasGrounded = isGrounded;
            // Allow jump if grounded or double jumping
            if (isGrounded || jumpCount < maxJumpCount)
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Catapult")
        {
            rb.velocity = new Vector3(catapultXValue, catapultYValue, 0);
        }

        if (other.gameObject.tag == "SpeedPad")
        {
            moveSpeed = 5f;
            StartCoroutine(IncreaseSpeed());
        }
    }

    IEnumerator IncreaseSpeed()
    {
       yield return new WaitForSeconds(10);
       moveSpeed = originalMoveSpeed;
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
        //gets the player's velocity
        v3Velocity = rb.velocity;
        
    }

    // Trigger the dust particle effect when the player lands
    private void TriggerDustAffect()
    {
        // Ensure that dust particles are only triggered when the player actually lands
        if (dustParticleSystem != null)
        {
            // Trigger the dust effect only when grounded
            dustEmission.enabled = true;
            dustParticleSystem.Play();
            Debug.Log("Dust particle system triggered.");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            audioSources[groundAudioIndex].Play();
            TriggerDustAffect();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
