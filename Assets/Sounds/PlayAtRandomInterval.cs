using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAtRandomInterval : MonoBehaviour
{
    public AudioClip clip;
    public float soundDistance = 8.0f;
    public float minInterval = 4.0f;
    public float maxInterval = 15.0f;
    private AudioSource source;

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1.0f;
        source.maxDistance = soundDistance;
        source.dopplerLevel = 0.0f;
        source.clip = clip;
        Invoke("PlaySound", Random.Range(minInterval, maxInterval));
    }

    void PlaySound()
    {
        source.Play();
        Invoke("PlaySound", Random.Range(minInterval, maxInterval));
    }
}
