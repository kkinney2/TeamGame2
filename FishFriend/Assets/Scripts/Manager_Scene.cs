using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager_Scene : MonoBehaviour {

    public Text ErrorText;

    private void Start()
    {
        SetErrorText("");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetSceneByBuildIndex(GetCurrentSceneIndex() + 1) != null)
        {
            LoadByIndex(GetCurrentSceneIndex() + 1); 
        }
        else
        {
            SetErrorText("Error Loading Level " + GetCurrentSceneIndex() + 1);
        }
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    void SetErrorText(string str)
    {
        ErrorText.text = str;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("_MainMenu_");
    }
}
