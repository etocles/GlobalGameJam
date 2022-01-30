using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public bool isStartPortal = false;
    private bool isEndPortal => !isStartPortal;
    [Space]
    // Stuff for level progression
    public string nextScene = "";
    private int finalTally = 0;
    [Space]
    public AudioClip portal_open;
    public AudioClip portal_close;
    public AudioClip levelComplete;
    public AudioClip teleport;
    public AudioSource source;
    // stuff for disappearing and reappearing
    private Vector3 scaleToResume = new Vector3();
    


    // Start is called before the first frame update
    void Start()
    {
        // configure self based on whether its a start portal or end portal
        scaleToResume = transform.localScale;
        if (isStartPortal) StartCoroutine("Disappear"); 
        if (isEndPortal) StartCoroutine("Appear");
    }

    private void PlayCloseSound() {
        source.clip = portal_close;
        source.Play();
    }
    private void PlayOpenSound()
    {
        source.clip = portal_open;
        source.Play();
    }
    public IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.5f);          // delay before doing anything
        Vector3 scaleChange = new Vector3(-0.001f, -0.001f, -0.001f);
        PlayCloseSound();
        while (transform.localScale.y >= 0.0f)
        {
            print("scale: " + transform.localScale);
            transform.localScale += scaleChange; // shrink it until it disappears
            yield return new WaitForEndOfFrame();
        }
        gameObject.GetComponent<Renderer>().enabled = false;
    }
    public IEnumerator Appear()
    {
        gameObject.GetComponent<Renderer>().enabled = false;    // become invisible
        transform.localScale -= scaleToResume;                  // set scale to 0,0,0
        yield return new WaitForSeconds(0.65f);          // delay before doing anything
        gameObject.GetComponent<Renderer>().enabled = true;
        Vector3 scaleChange = new Vector3(0.001f, 0.001f, 0.001f);
        PlayOpenSound();
        while (transform.localScale.y <= scaleToResume.y)
        {
            transform.localScale += scaleChange; // shrink it until it disappears
            yield return new WaitForEndOfFrame();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isStartPortal) return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Elephant"))
        {
            print("ELEPHANT GOT TO THE END");
            //StartCoroutine("EndLevel");
        }
    }

    public IEnumerator EndLevel()
    {
        yield break;
        // play victory sound
        source.clip = levelComplete;
        source.Play();
        while (source.isPlaying) { yield return new WaitForEndOfFrame(); }
        // additively load LevelComplete Screen
        source.clip = teleport;
        source.Play(); 
        SceneManager.LoadScene(nextScene, LoadSceneMode.Additive);
        //yield return StartCoroutine(TallyEggs());
    }

    private void TallyEggs()
    {

    }
}
