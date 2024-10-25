using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinner : MonoBehaviour
{
    public float xAngle = 0;
    public float yAngle = 0;
    public float zAngle = 0;
    public int rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xAngle * rotateSpeed, yAngle * rotateSpeed, zAngle * rotateSpeed);    
    }
}
