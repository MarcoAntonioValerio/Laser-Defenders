using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scp_SceneLoader : MonoBehaviour
{

    int currentSceneIndex;
    scp_GameManager gameMan;

    private void Update()
    {
        Win();
    }

    private void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        gameMan = FindObjectOfType<scp_GameManager>();
    }

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
        Debug.Log("Quit() fired");
    }

    public void LoadSpecificScene(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Win()
    {
        if (gameMan.totalScore >= 10000f)
        {
            LoadNextScene();
        }
    }
}
