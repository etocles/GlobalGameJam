using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject mainPanel;
    public TMP_Text eggText;

    private void Start()
    {
        gameObject.SetActive(false);
        instance = this;
    }

    public void SetEggCount(int eggCount)
    {
        gameObject.SetActive(true);
        eggText.text = "YOU DID IT, YOU BEAUTIFUL OAF!\nYOU GOT: " + eggCount + " EGGS TO THE END!";
    }
}
