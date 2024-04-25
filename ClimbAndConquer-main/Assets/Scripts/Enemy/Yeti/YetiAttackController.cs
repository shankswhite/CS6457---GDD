using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiAttackController : MonoBehaviour
{
    public AudioClip hitSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Stamina playerStamina = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<Stamina>();
            PlayHitSound();
            if (playerStamina != null)
            {
                playerStamina.takeDamage();
                Debug.Log("Stamina decreased.");
            }
            else
            {
                Debug.Log("Stamina component not found on Player.");
            }
        }
    }

    void PlayHitSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = hitSound;
            audioSource.Play();
        }
    }
}
