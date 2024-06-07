using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    
    public void loadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within the valid range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Next scene index is out of range.");
        }
    }

    public void loadFirstLevel()
    {
        Debug.Log("Loading back to first level");
        SceneManager.LoadScene("LevelScene");


    }

    public void quitGame()
    {
        Debug.Log("Ending");
        SceneManager.LoadScene("EndScene");

    }
}
