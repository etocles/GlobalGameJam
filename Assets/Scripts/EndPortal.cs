using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EggContainer")
        {
            int eggCount = other.GetComponent<EggContainer>().GetEggCount();
            UIManager.instance.InformOfFinish(eggCount);
            HandleFinish();
        }
    }

    private void HandleFinish()
    {
        // TODO
    }
}
