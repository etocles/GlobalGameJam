using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Start()
    {
        if (instance != null) {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void InformOfFinish(int eggCount)
    {

    }
}
