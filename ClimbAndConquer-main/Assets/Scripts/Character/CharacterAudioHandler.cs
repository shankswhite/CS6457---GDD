using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioHandler : MonoBehaviour
{
    [SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip runSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip climbSound;
    private bool isMoveSoundPlaying;
    private bool isShootSoundPlaying;
    private bool isHurtSoundPlaying;

    private AudioSource audioSource;

    private AmmoSystem ammoSystem;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ammoSystem = GetComponent<AmmoSystem>();
    }


    public void PlayWalkSound()
    {
        if (!isMoveSoundPlaying)
        {
        
            audioSource.clip = walkSound;
            audioSource.Play();
            audioSource.loop = false;
        }
        
    }


    public void PlayRunSound()
    {
        if (!isMoveSoundPlaying)
        {
            audioSource.clip = runSound;
            audioSource.Play();
            audioSource.loop = false;
            isMoveSoundPlaying = true;
        }

    }


    public void PlayShootSound()
    {
        if (!isShootSoundPlaying && ammoSystem.hasAmmo())
        {
            audioSource.clip = shootSound;
            audioSource.volume = 0.2f;
            audioSource.Play();
            audioSource.loop = false;
            //isShootSoundPlaying = true;
        }
        
    }


    public void PlayJumpSound()
    {
        if (!isMoveSoundPlaying)
        {
            audioSource.clip = climbSound;
            audioSource.Play();
            audioSource.loop = false;

            isMoveSoundPlaying = true;
        }
    

    }

    public void PlayClimbSound()
    {
        if (!isMoveSoundPlaying)
        {
            audioSource.clip = climbSound;
            audioSource.Play();
            audioSource.loop = false;
            isMoveSoundPlaying = true;
        }

    }
    public void PlayHurtSound()
    {
        audioSource.clip = hurtSound;
        audioSource.Play();
        audioSource.loop = false;           
    }

    public void EnableMoveSound()
    {
        isMoveSoundPlaying = false;
    }

    public void DisableMoveSound()
    {
        isMoveSoundPlaying = true;
    }

    public void StopMoveSound()
    {
        audioSource.Stop();
        isMoveSoundPlaying = true;
    }

    public void StopShootSound()
    {
        audioSource.Stop();
        isShootSoundPlaying = false;
    }
}
