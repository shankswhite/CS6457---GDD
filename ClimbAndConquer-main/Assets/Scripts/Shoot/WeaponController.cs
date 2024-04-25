using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public bool isFiring;

    public ParticleSystem muzzleFlash;

    [SerializeField] Transform raycastStartPoint;
    [SerializeField] Transform raycastEndPoint;


    private Ray ray;
    private RaycastHit hitInfo;
    private GameObject playerObject;
    private AmmoSystem ammoSystem;


    [SerializeField] GameObject bananas;
    [SerializeField] float shootForce = 20f;

    private void Awake()
    {
        this.playerObject = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(this.playerObject);
        ammoSystem = this.playerObject.GetComponent<AmmoSystem>();
    }



    public void StartFiring()
    {
        if (ammoSystem.hasAmmo())
        {
            isFiring = true;
            muzzleFlash.Emit(1);
            //raycastStartPoint.position = raycastStartPoint.position + new Vector3(0, 0.5f, 0);
            var shootDirection = raycastEndPoint.position - raycastStartPoint.position;

            GameObject initBanana = Instantiate(bananas, raycastStartPoint.position, raycastStartPoint.rotation);
            initBanana.AddComponent<Projectiles>();
            Physics.IgnoreCollision(initBanana.GetComponent<Collider>(), this.playerObject.GetComponent<Collider>());

            ammoSystem.useAmmo();

            //From Hamzah- Note to self, is this where I need to reduce the ammo count?
            //From Levon- Doing the same thing as our Stamina Syste should work.
            Rigidbody rb = initBanana.GetComponent<Rigidbody>();
            Debug.DrawRay(raycastStartPoint.position, raycastStartPoint.forward, Color.red, 2.0f);
            if (rb != null)
            {
                //rb.AddForce(200 * raycastStartPoint.forward * shootForce, ForceMode.Impulse);
                rb.AddForce(3f *  shootDirection * shootForce, ForceMode.Impulse);

            }
        }


    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(raycastStartPoint.position, raycastStartPoint.forward * 10);
    }

    public void StopFiring()
    {
        isFiring = false;
    }


}
