using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public string[] scenes;
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(scenes[level]);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
