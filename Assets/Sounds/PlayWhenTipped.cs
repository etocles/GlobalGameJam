using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayWhenTipped : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;
    public AudioClip fall;
    public AudioClip thud;
    public float soundDistance = 8.0f;
    private AudioSource source;
    private bool fallen = false;

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1.0f;
        source.dopplerLevel = 0.0f;
        source.outputAudioMixerGroup = mixerGroup;
        source.maxDistance = soundDistance;
        source.clip = fall;
    }

    void Update()
    {
        if (!fallen && transform.up.y < 0.8f && !source.isPlaying) //1.0f is straight up, 0.0f is laying down
        {
            source.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (fallen) return; // only play the thud sound once
        if (transform.up.y > 0.3f) return; // if not close to horizontal, don't play sound and don't mark as fallen
        fallen = true;
        source.clip = thud;
        source.Play();
    }
}
