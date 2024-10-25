using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation2 : MonoBehaviour
{
    [SerializeField] float XRotateSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        rotatecamera();
    }

    void rotatecamera()
    {   
        float xrValue = Input.GetAxis("Mouse X") * Time.deltaTime * XRotateSpeed;
        transform.Rotate(0, xrValue, 0);
    }
}
