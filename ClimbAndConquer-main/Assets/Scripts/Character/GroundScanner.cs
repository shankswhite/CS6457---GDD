using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScanner : MonoBehaviour
{
    [SerializeField] Vector3 forwardRayOffset = new Vector3(0, 2.5f, 0);
    [SerializeField] float forwardRayLength = 0.8f;
    [SerializeField] float heightRayLength = 5f;
    [SerializeField] LayerMask obstaclelayer;
    public ObstacleHitData ObstacleCheck()
    {
        var hitData = new ObstacleHitData();
        Vector3 rayOrigin = transform.position + forwardRayOffset;
        hitData.forwardHitFound = Physics.Raycast(rayOrigin, transform.forward, out hitData.forwardHit,
            forwardRayLength, obstaclelayer);

        Debug.DrawRay(rayOrigin, transform.forward * forwardRayLength, (hitData.forwardHitFound) ? Color.red : Color.white);

        if (hitData.forwardHitFound)
        {
            var heightOrigin = hitData.forwardHit.point + Vector3.up * heightRayLength;
            hitData.heightHitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitData.heightHit,
                heightRayLength, obstaclelayer);
            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (hitData.heightHitFound) ? Color.red : Color.white);
        }


        return hitData;
    }
}


public struct ObstacleHitData
{
    public bool forwardHitFound;
    public bool heightHitFound;
    public RaycastHit forwardHit;
    public RaycastHit heightHit;
}