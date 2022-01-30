using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileEgg : MonoBehaviour
{
    public GameObject broken_egg;
    public AudioClip[] clips;
    private float soundDistance = 8.0f;
    private AudioSource source;

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1.0f;
        source.maxDistance = soundDistance;
    }

    // when colliding, if velocity is big enough, play crack sound, and shatter
    void OnCollisionEnter(Collision collision)
    {
        // if soft, don't crack
        if (collision.impulse.magnitude < 4) return;

        // select a clip to play from the list
        source.clip = clips[UnityEngine.Random.Range(0, clips.Length - 1)];
        // play it
        source.Play();
        // shatter
        Shatter();
    }

    public void Shatter()
    {
        // create the two shells
        Instantiate(broken_egg, transform);
        // disable own mesh and colliders
        Destroy(GetComponent<SphereCollider>());
        Destroy(GetComponent<Rigidbody>());
        // invoke Destroy after like 10 seconds
        Destroy(gameObject, 10f);
    }

    public void SetContainer(EggContainer ec)
    {
        // TODO
    }
}
