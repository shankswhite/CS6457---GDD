using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class RockDespawner : MonoBehaviour
{
    public float despawnDelay = 2f;
    public float stillThreshold = 0.1f; // Velocity

    private Rigidbody rb;
    private bool isWaitingToDespawn = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the rock has nearly stopped moving
        if (!isWaitingToDespawn && rb.velocity.magnitude < stillThreshold)
        {
            isWaitingToDespawn = true;
            Invoke(nameof(Despawn), despawnDelay);
        }
    }

    private void Despawn()
    {
        // Make sure that the rock has stopped
        if (rb.velocity.magnitude < stillThreshold)
        {
            Destroy(gameObject);
        }
        else
        {
            isWaitingToDespawn = false; // If the rock is still moving, cancel the despawn
        }
    }
}
