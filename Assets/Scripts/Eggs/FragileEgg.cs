using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FragileEgg : MonoBehaviour
{
    public AudioMixerGroup sfx_group;
    public GameObject broken_egg;
    public AudioClip[] clips;
    private float soundDistance = 8.0f;
    private AudioSource source;
    private bool is_breakable = false;
    private EggContainer container = null;

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1.0f;
        source.maxDistance = soundDistance;
        try
        {
            source.outputAudioMixerGroup = sfx_group;
        }
        finally { }
        Invoke("enable_breaking", 1f);
    }

    private void enable_breaking()
    {
        is_breakable = true;
    }

    // when colliding, if velocity is big enough, play crack sound, and shatter
    void OnCollisionEnter(Collision collision)
    {
        // if in spawn grace period, not breakable
        if (!is_breakable) return;
        // if another egg, don't crack against it
        if (collision.gameObject.layer == 8) return;
        // if soft, don't crack
        //if (collision.relativeVelocity.magnitude < 2.0) return;
        if (collision.gameObject.layer == 6) Shatter(); // if the elephant stepped on you, break.
        if (collision.impulse.magnitude < 0.1) return;
        if (container != null && collision.impulse.magnitude < 0.2) return;
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8) return; // Container layer and egg layer

        
        // shatter
        Shatter();
    }

    public void Shatter()
    {
        // select a clip to play from the list
        source.clip = clips[UnityEngine.Random.Range(0, clips.Length - 1)];
        // play it
        source.Play();
        // create the two shells
        Instantiate(broken_egg, transform,true);
        // disable own mesh and colliders
        // GetComponent<SphereCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        //Destroy(GetComponent<Rigidbody>());
        // invoke Destroy after like 10 seconds
        //Destroy(gameObject, 10f);
    }

    public void SetContainer(EggContainer ec)
    {
        container = ec;
    }
}
