using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honk : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;
    private int last_int = 0;

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            // never play the same sound twice
            int new_int = Random.Range(0, clips.Length - 1);
            while (last_int == new_int)
            {
                new_int = Random.Range(0, clips.Length - 1);
            }
            source.clip = clips[last_int];
            source.Play();
            last_int = new_int;
        }
    }
}
