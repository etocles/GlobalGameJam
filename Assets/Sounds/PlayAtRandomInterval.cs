using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayAtRandomInterval : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;
    public AudioClip[] clips;
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
        source.outputAudioMixerGroup = mixerGroup;
        Invoke("PlaySound", Random.Range(minInterval, maxInterval));
    }

    void PlaySound()
    {
        source.clip = clips[Random.Range(0, clips.Length - 1)]; 
        source.Play();
        Invoke("PlaySound", Random.Range(minInterval, maxInterval));
    }
}
