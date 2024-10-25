using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation2 : MonoBehaviour
{
    public float XRotateSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        Rotatecamera();
    }

    void Rotatecamera()
    {   
        float xrValue = Input.GetAxis("Mouse X") * Time.deltaTime * XRotateSpeed;
        transform.Rotate(0, xrValue, 0);
    }
}
