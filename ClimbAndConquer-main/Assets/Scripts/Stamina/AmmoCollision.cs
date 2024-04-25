using UnityEngine;
using System.Collections;

public class AmmoCollision : MonoBehaviour
{
    private bool isActive = true;

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Collision detected with: " + collision.gameObject.name);

        
        if (isActive)
        {
            isActive = false;
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Collision with Player detected");
                
                Stamina playerStamina = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<Stamina>();
                if (playerStamina != null)
                {
                    playerStamina.takeDamage();
                    Debug.Log("Stamina decreased");
                }
                else
                {
                    Debug.Log("Stamina component not found on Player");
                }

                CharacterAudioHandler characterAudioHandler = collision.gameObject.GetComponent<CharacterAudioHandler>();
                if (characterAudioHandler != null)
                {
                    characterAudioHandler.PlayHurtSound();
                }
                else
                {
                    Debug.Log("Character audio component not found on player after ammo hit them.");
                }
            }
            
            

        }
    }
}
