using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 10f;
    [SerializeField] float XRotateSpeed = 10f;
    [SerializeField] float YRotateSpeed = 10f;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    Rigidbody rb;
    public float yrValue;
    float xrValue;
    // Update is called once per frame
    void Update()
    {
        moveplayer();
        rotateplayer();
        
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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    //when the player is colliding with the ground, is grounded = true
    void OnCollisionStay()
    {
        isGrounded = true;
    }
   
     void moveplayer()
    {
        //moves the player when WASD is pressed and changes the movement speed based the the value of the variable MoveSpeed
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed;
        transform.Translate(zValue, 0, xValue);
    }

    void rotateplayer()
    {
        xrValue = Input.GetAxis("Mouse X") * XRotateSpeed * Time.deltaTime;
        yrValue = Input.GetAxis("Mouse Y") * YRotateSpeed * Time.deltaTime;

        //allows the player to turn horizontally by using the mouse

        //transform.Rotate(0, xrValue, 0);
        //allows the player to turn vertically by using the mouse
        transform.Rotate(0f, xrValue,0f);
    }
}
