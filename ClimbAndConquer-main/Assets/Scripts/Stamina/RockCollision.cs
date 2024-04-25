using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with Player detected");
            Stamina playerStamina = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<Stamina>();
            if (playerStamina != null)
            {
                playerStamina.takeDamage();
                Debug.Log("Stamina decreased.");
                CharacterAudioHandler characterAudioHandler = collision.gameObject.GetComponent<CharacterAudioHandler>();
                if (characterAudioHandler != null)
                {
                    characterAudioHandler.PlayHurtSound();
                }
            }
            else
            {
                Debug.Log("Stamina component not found on Player.");
            }
        }
    }
}
