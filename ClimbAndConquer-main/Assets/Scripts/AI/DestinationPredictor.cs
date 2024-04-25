using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPredictor : MonoBehaviour
{
    public Transform character;
    public float distanceFromCharacter = 30f;

    private Vector3 destinationPosition;
    private Vector3 characterPosition;
    // private float speedThreshold = 0.1f;
    private float maxDistanceFromCharacter = 50f;
    private float minDistanceFromCharacter = 10f;

    void Start()
    {
        if (character)
        {
            Vector3 characterForward = character.forward.normalized;
            destinationPosition = character.position + characterForward * distanceFromCharacter;
            transform.position = destinationPosition;
            characterPosition = character.position;
        }
        else
        {
            Debug.Log("User Character not attached");
        }
    }

    void Update()
    {
        SmoothRotationUpdate();
        SmoothDistanceUpdate();
    }

    void SmoothRotationUpdate()
    {
        Vector3 characterForward = character.forward.normalized;
        Vector3 newPosition = character.position + characterForward * distanceFromCharacter;

        if (newPosition != destinationPosition)
        {
            destinationPosition = newPosition;
            if (!IsVector3NaN(destinationPosition))
            {
                // Debug.Log(destinationPosition);
                transform.position = destinationPosition;
            }
        }
        Quaternion targetRotation = Quaternion.LookRotation(characterForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
    }

    bool IsVector3NaN(Vector3 vector)
    {
        return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z);
    }

    void SmoothDistanceUpdate()
    {
        float characterSpeed = Vector3.Distance(character.position, characterPosition) / Time.deltaTime;

        //if (characterSpeed > speedThreshold)
        //{
        float newDistance = Mathf.Clamp(characterSpeed * 2f, minDistanceFromCharacter, maxDistanceFromCharacter);
        distanceFromCharacter = Mathf.Lerp(distanceFromCharacter, newDistance, Time.deltaTime * 2f);
        //}

        characterPosition = character.position;
    }

    public Vector3 GetPrediction()
    {
        return transform.position;
    }
}


