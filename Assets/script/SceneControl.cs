using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager class



public class scenecontrol : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Game Ended");
        Application.Quit();
        
    }
    public void RestartScene()
    {
        // Get the current scene's index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the scene
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }


}
