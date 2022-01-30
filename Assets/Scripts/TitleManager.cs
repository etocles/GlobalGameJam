using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public string[] scenes;

    public TMP_Text[] highScores;

    private void Start()
    {
        for (int i = 0; i < highScores.Length; ++i)
        {
            if (PlayerPrefs.HasKey("MaxScene" + (i + 2)))
                highScores[i].text = "Egg Score: " + PlayerPrefs.GetInt("MaxScene" + (i + 2));
            else
                highScores[i].text = "No Egg Score";
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(scenes[level]);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
