using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OnHitPlaySound : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;
    public AudioClip[] clips;
    public float soundDistance = 8.0f;
    private AudioSource source;

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1.0f;
        source.dopplerLevel = 0.0f;
        source.outputAudioMixerGroup = mixerGroup;
        source.maxDistance = soundDistance;
    }

    void OnCollisionEnter(Collision collision)
    {
        source.clip = clips[Random.Range(0, clips.Length-1)];
        source.Play();
    }
}
