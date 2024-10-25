using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float xRotateSpeed = 10f;
    public float yRotateSpeed = 10f;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    private Rigidbody rb;
    private float yrValue;
    private float xrValue;
    private Camera _mainCamera;

    // Update is called once per frame
    private void Update()
    {
        Moveplayer();
        Rotateplayer();
        
        //if (yrValue < minyrValue)
        {
            //yrValue = 0.1f;
        }
        //else if (yrValue > maxyrValue)
        {
            //yrValue = 99f;
        }

        //when the player is on the ground and presses space, the player jumps
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        _mainCamera = Camera.main;
    }

    //when the player is colliding with the ground, is grounded = true
    private void OnCollisionStay()
    {
        isGrounded = true;
    }
   
    private void Moveplayer()
    {
        //moves the player when WASD is pressed and changes the movement speed based the the value of the variable moveSpeed
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 cameraForward = Vector3.Scale(_mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = (cameraForward * moveVertical + _mainCamera.transform.right * -moveHorizontal).normalized;        
        rb.AddForce(movement * moveSpeed);
        //Vector3 movement = new Vector3(moveVertical, moveHorizontal, 0f).normalized;
        //float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
        //float zValue = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;
        //transform.Translate(zValue, 0, xValue);

    }

    private void Rotateplayer()
    {
        xrValue = Input.GetAxis("Mouse X") * xRotateSpeed * Time.deltaTime;
        yrValue = Input.GetAxis("Mouse Y") * yRotateSpeed * Time.deltaTime;

        //allows the player to turn horizontally by using the mouse

        //transform.Rotate(xrValue, yrValue, 0);
        //allows the player to turn vertically by using the mouse
        transform.Rotate(0f, xrValue,0f);
    }
}
