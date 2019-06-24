using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scp_SceneLoader : MonoBehaviour
{
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);    
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
