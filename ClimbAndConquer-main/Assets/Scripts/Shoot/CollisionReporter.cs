using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionReporter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("######## Player Banana Collision Enter with: " + collision.gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("######## Player Banana Collision Stay with: " + collision.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("######## Player Banana Collision Exit with: " + collision.gameObject.name);
    }
}
