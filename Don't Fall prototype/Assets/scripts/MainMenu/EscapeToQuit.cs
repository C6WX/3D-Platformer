using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeToQuit : MonoBehaviour
{
    void Update()
    {
        //closes the game when escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quitting Game");
            Application.Quit();
        }
    }
}
