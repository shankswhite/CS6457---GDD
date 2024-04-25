using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private Transform referencePoint; // projectile的初始位置
    private bool isActive = true;
    public float maxDistance = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        referencePoint = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, referencePoint.position);

        if (distance > maxDistance)
        {
            Destroy(gameObject);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Enemy"))
        {
            MonkeyMovement monkeyMovement = other.gameObject.GetComponentInParent<MonkeyMovement>();
            Debug.Log("sidtest hit an enemy." + monkeyMovement);

            if (monkeyMovement != null)
            {
                //    Destroy(other.gameObject);
                //    Destroy(other.gameObject.transform.parent.gameObject);
                monkeyMovement.onKilled();
            }
            
            //Debug.Log("mixi");

        } else
        {
            isActive = false;
        }
    }
}
