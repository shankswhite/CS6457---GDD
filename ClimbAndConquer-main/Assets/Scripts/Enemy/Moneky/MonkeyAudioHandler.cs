using UnityEngine;
using System.Collections;

public class MonkeyAudioHandler : MonoBehaviour
{
    [SerializeField] AudioClip tauntSound;
    [SerializeField] AudioClip enemyDetectedSound;
    [SerializeField] AudioClip ammoThrownSound;
    [SerializeField] AudioClip monkeyRunningSound;
    [SerializeField] AudioClip monkeyKilledSound;


    private AudioSource audioSource;



    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayTauntSound()
    {
        Debug.Log("sidtest playing taunt sound");
        audioSource.clip = tauntSound;
        audioSource.Play();
        audioSource.loop = false;
    }

    public void PlayEnemyDetectedSound()
    {
        audioSource.clip = enemyDetectedSound;
        audioSource.Play();
        audioSource.loop = false;
    }

    public void PlayAmmoThrownSound()
    {
        Debug.Log("sidtest playing ammo thrown sound");

        audioSource.clip = ammoThrownSound;
        audioSource.Play();
        audioSource.loop = false;
    }

    public void PlayRunningSound()
    {
        audioSource.clip = monkeyRunningSound;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void PlayKilledSound()
    {
        Debug.Log("sidtest playing monkey killed sound");

        audioSource.clip = monkeyKilledSound;
        audioSource.Play();
        audioSource.loop = false;
    }
}
