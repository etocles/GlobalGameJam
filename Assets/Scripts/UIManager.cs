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
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void SetEggCount(int eggCount)
    {
        eggText.text = "YOU DID IT, YOU BEAUTIFUL OAF!\nYOU GOT: " + eggCount + " EGGS TO THE END!";
    }
}
