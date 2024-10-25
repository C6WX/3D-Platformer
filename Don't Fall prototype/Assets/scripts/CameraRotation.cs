using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = playerObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        rotateCamera();
    }
    void rotateCamera()
    {
        transform.Rotate(-playerMovement.yrValue, 0f,0f);
        
    }
}
