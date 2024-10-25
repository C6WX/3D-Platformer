using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour
{
    [SerializeField] float LoadDelay = 1f;

    private void OnCollisionEnter(Collision other)
    {
        //when the player touches the plane, the level restarts
        if (other.gameObject.tag == "Player")
        {
            Invoke("ReloadLevel", LoadDelay);
        }
    }
    void ReloadLevel()
    {
        //Reloads the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
