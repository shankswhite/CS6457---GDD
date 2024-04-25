using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiAudioController : MonoBehaviour
{
    public Transform characterTransform; 
    public float detectionDistance = 10f; 

    public AudioClip yetiSound; 
    private AudioSource audioSource; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, characterTransform.position) <= detectionDistance)
        {
            PlayYetiSound();
        }
    }

    void PlayYetiSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = yetiSound;
            audioSource.Play();
        }
    }
}
