using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip hit;
    public AudioClip miss;
    public AudioClip squidDead;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SquidHit()
    {
        audioSource.PlayOneShot(hit, 0.7F);
    }

    public void Miss()
    {
        audioSource.PlayOneShot(miss, 0.7F);
    }
    public void SquidDead()
    {
        audioSource.PlayOneShot(squidDead, 0.7F);
    }

}
