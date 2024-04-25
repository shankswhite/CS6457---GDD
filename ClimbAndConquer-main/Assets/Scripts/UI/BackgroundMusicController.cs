using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public float fadeStartDuration = 10.0f;
    public float fadeEndDuration = 5.0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
        }
        audioSource.loop = true;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        audioSource.volume = 0f;

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += Time.deltaTime / fadeStartDuration;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while (audioSource.volume > 0.0f)
        {
            audioSource.volume -= Time.deltaTime / fadeEndDuration;
            yield return null;
        }
        audioSource.Stop();
    }
}

