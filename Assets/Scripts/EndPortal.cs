using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Elephant")
        {
            int eggCount = other.GetComponent<Movement>().eggContainer.GetEggCount();
            print("YOU GOT TO END WITH : " + eggCount);
            UIManager.instance?.InformOfFinish(eggCount);
            HandleFinish();
        }
    }

    private void HandleFinish()
    {
        // TODO
    }
}
