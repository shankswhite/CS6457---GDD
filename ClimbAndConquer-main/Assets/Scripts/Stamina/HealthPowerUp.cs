using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{


    //public Stamina staminaBar;
    //public StaminaBar StaminaBar;
    public Stamina Stamina;
    //update



    void OnTriggerEnter(Collider c)
    {
        //this is code inspired from the milestone projects.  It checks to see if the thing colliding with it has the "PowerupCollector" tag and if so it will delete itself.
        PowerupCollector playerPowerupCollector = c.attachedRigidbody.gameObject.GetComponent<PowerupCollector>();
        if (playerPowerupCollector != null)
        {
            //EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.transform.position);
            Destroy(this.gameObject);
            //staminaBar.eatFood();
            Stamina.eatFood();
        }


        // }
    }
}