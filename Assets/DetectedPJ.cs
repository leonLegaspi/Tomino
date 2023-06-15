using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedPJ : MonoBehaviour
{
    private AudioSource myAudioSource;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!myAudioSource.isPlaying)
                myAudioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            myAudioSource.Stop();
    }
}
