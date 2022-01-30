using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortal : MonoBehaviour
{
    public float eggPopTime = 0.05f;
    public float finalWaitTime = 1f;
    public AudioSource source;
    public AudioClip eggSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Elephant")
        {
            GetComponent<Collider>().enabled = false;
            UIManager.instance?.SetEggCount(0);
            other.GetComponent<Movement>().enabled = false;
            other.GetComponent<TrunkAimer>().enabled = false;
            other.GetComponent<Movement>().eggContainer.FreezeEggs();
            StartCoroutine(IHandleFinish(other.GetComponent<Movement>().eggContainer, other));
        }
    }

    IEnumerator IHandleFinish(EggContainer container, Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
        int eggCount = 0;
        var eggs = container.GetEggs();
        foreach (FragileEgg egg in eggs)
        {
            UIManager.instance?.SetEggCount(++eggCount);
            egg.gameObject.SetActive(false);
            source.PlayOneShot(eggSound);
            yield return new WaitForSeconds(eggPopTime);
            other.GetComponent<Rigidbody>().velocity *= 0.95f;
        }

        yield return new WaitForSeconds(finalWaitTime);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, eggCount);
        SceneManager.LoadScene(0); // TITLE
    }
}
