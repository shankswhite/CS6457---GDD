using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoSystem : MonoBehaviour
{
    // Start is called before the first frame update
    // need an empty ammo interaction.
    //need to visualize the ammo system on the screen.

    public int ammoCount;
    public int ammoMaxAmount = 20;
    public Slider ammoSlider;
    public bool hasAmmoBoolean;
    


    void Start()
    {
        ammoCount = ammoMaxAmount;
        ammoSlider.value = ammoCount;
        hasAmmoBoolean = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            useAmmo();
            //decreaseStamina(100);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            refillAmmo();
            //decreaseStamina(100);
        }
    }
    public bool hasAmmo()
    {
        if (ammoCount <= 0) {
            hasAmmoBoolean = false;
            return hasAmmoBoolean;
        }
        else {
            hasAmmoBoolean = true;
            return hasAmmoBoolean; 
        }
    }

    public void updateHasAmmo() {
        if (ammoCount <= 0)
        {
            hasAmmoBoolean = false;
        }
        else
        {
            hasAmmoBoolean = true;
        }
    }

    public void useAmmo()
    {
        if (hasAmmo())
        {
            ammoCount--;
            ammoSlider.value = ammoCount;
            updateHasAmmo();
        }
    }

    public void shootingWithoutAmmo()
    {

    }

    public void refillAmmo()
    {
        ammoCount = ammoMaxAmount;
        ammoSlider.value = ammoCount;
        hasAmmoBoolean = true;
        updateHasAmmo();

    }
}
