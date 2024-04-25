using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    private WeaponController weapon;
    private CharacterInput cinput;
    private Animator anim;


    private void Start()
    {
        weapon = GetComponentInChildren<WeaponController>();
        cinput = GetComponent<CharacterInput>();
        anim = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (cinput.Fire & anim.GetBool("StrappedUp"))
        {
            weapon.StartFiring();
        }
        //might need to add feedback if players are out of ammo.
        if (cinput.Cease)
        {
            weapon.StopFiring();
        }
    }
}
