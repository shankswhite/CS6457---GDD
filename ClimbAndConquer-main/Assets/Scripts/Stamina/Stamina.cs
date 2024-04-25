using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stamina : MonoBehaviour
{
    // Start is called before the first frame update

    public int StaminaValue;
    public int FoodStaminaIncrease;
    public int DamageStaminaDecrease;
    public int WalkingStaminaDecrease;
    public int RunningStaminaDecrease;
    public int JumpingStaminaDecrease;

    public bool isInvincible;
    public bool isGameOver;

    public Slider slider;

    public GameObject player;
    //public StaminaBar StaminaBar;



    //instantiates all the values for the different damages/health regains.  Tuning can be done here.
    void Awake()
    {
        StaminaValue = 1000;
        FoodStaminaIncrease = 100;
        DamageStaminaDecrease = 100;
        isInvincible = false;
        isGameOver = false;
        WalkingStaminaDecrease = 10;
        RunningStaminaDecrease = 15;
        JumpingStaminaDecrease = 10;
        slider.value = StaminaValue;
    }



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            takeDamage();
            decreaseStamina(100);
        }
    }




    //increaseStamina() and decreaseStamina() are called by anything updating the stamina bar.


    public void increaseStamina(int StaminaIncrease)
    {
        StaminaValue = StaminaValue + StaminaIncrease;
        slider.value = StaminaValue;

    }

    public void endGame()
    {

    }


    public void decreaseStamina(int StaminaDecrease)
    {
        Debug.Log("old stamine val: " + StaminaValue + ", decrease=" + StaminaDecrease);
        StaminaValue = StaminaValue - StaminaDecrease;
        slider.value = StaminaValue;
        if (StaminaValue <= 0){
            isGameOver = true;
            //Insert code to popup a gameover screen
        }
    }

    public bool isGameOverStatus()
    {
        return isGameOver;
    }

    //The following functions call increaseStamina() and descreaseStamina().

    //takeDamage() has a clause to check if isInvincble is true and ifso it wont lower stamina

    public void eatFood()
    {
        increaseStamina(FoodStaminaIncrease);

    }

    public void takeDamage()
    {
        if (isInvincible == false)
        {
            decreaseStamina(DamageStaminaDecrease);
        }
        if (isInvincible == true) { 
            endInvincible();
        }
    }

    public void walkUpdateStamina()
    {
        decreaseStamina(WalkingStaminaDecrease);
    }

    public void runningUpdateStamina()
    {
        decreaseStamina(RunningStaminaDecrease);
    }

    public void jumpingUpdateStamina()
    {
        decreaseStamina(JumpingStaminaDecrease);
    }


    //goInvicible() and endInvincible() flip the isInvincble boolean.

    public void goInvincible()
    {
        isInvincible = true;
    }

    public void endInvincible()
    {
        isInvincible = false;
    }




}
