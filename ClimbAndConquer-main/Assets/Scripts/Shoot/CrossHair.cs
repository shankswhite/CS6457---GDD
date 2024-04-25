using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hitInfo;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << LayerMask.NameToLayer("player");
        layerMask = ~layerMask;

        RaycastHit hitInfo;
        Vector3 rayOrigin = mainCamera.transform.position;
        Vector3 rayDirection = mainCamera.transform.forward;


        Debug.DrawRay(rayOrigin, rayDirection * 1000, Color.red);


        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo, Mathf.Infinity, layerMask))
        {

            transform.position = hitInfo.point;
        }
    }
}
