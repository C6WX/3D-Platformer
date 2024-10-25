using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNextScene : MonoBehaviour
{
    private int levelLoadDelay = 0;

    private void OnCollisionEnter(Collision other)
    {
        //when the player touches the door, the next level is loaded
        if (other.gameObject.tag == "Player")
        {
            Invoke("LoadNextLevel", levelLoadDelay);
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
