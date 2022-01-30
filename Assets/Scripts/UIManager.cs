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
        string S = 
        eggText.text = "YOU DID IT, YOU BEAUTIFUL OAF!\nYOU GOT: " + eggCount + " EGG" + (eggCount == 1 ? "" : "s") + " TO THE END!";
    }
}
