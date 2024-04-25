using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;

    public bool InputMapToCircular = true;

    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;

    private float forwardSpeedLimit = 1f;
    //private float directionSpeed = 1.5f;

    //private int m_LocomotionId = 0;
    //private int m_LocomotionPivotLId = 0;
    //private int m_LocomotionPivotRId = 0;
    //private int m_LocomotionPivotLTransId = 0;
    //private int m_LocomotionPivotRTransId = 0;

    //private AnimatorStateInfo stateInfo;
    //private AnimatorTransitionInfo transInfo;

    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }

    public float RotationY
    {
        get;
        private set;
    }

    public float RotationX
    {
        get;
        private set;
    }

    public float LocomotionThreshold
    {
        get { return 0.2f; }
    }

    public bool Fire
    {
        get;
        private set;
    }

    public bool Cease
    {
        get;
        private set;
    }

    public bool Jump
    {
        get;
        private set;
    }

    public bool Crouch
    {
        get;
        private set;
    }

    public bool TurnHead
    {
        get;
        private set;
    }

    public bool Toggle
    {
        get;
        private set;
    }



    void Update()
    {

        //GetAxisRaw() so we can do filtering here instead of the InputManager
        float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
        float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis
        float rotationY = Input.GetAxis("Mouse X");
        float rotationX = Input.GetAxis("Mouse Y");
        
        if (InputMapToCircular)
        {
            // make coordinates circular
            //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);        

        }


        //BEGIN ANALOG ON KEYBOARD DEMO CODE
        if (Input.GetKey(KeyCode.Q))
            h = -0.5f;
        else if (Input.GetKey(KeyCode.E))
            h = 0.5f;

        if (Input.GetKeyUp(KeyCode.Alpha1))
            forwardSpeedLimit = 0.1f;
        else if (Input.GetKeyUp(KeyCode.Alpha2))
            forwardSpeedLimit = 0.2f;
        else if (Input.GetKeyUp(KeyCode.Alpha3))
            forwardSpeedLimit = 0.3f;
        else if (Input.GetKeyUp(KeyCode.Alpha4))
            forwardSpeedLimit = 0.4f;
        else if (Input.GetKeyUp(KeyCode.Alpha5))
            forwardSpeedLimit = 0.5f;
        else if (Input.GetKeyUp(KeyCode.Alpha6))
            forwardSpeedLimit = 0.6f;
        else if (Input.GetKeyUp(KeyCode.Alpha7))
            forwardSpeedLimit = 0.7f;
        else if (Input.GetKeyUp(KeyCode.Alpha8))
            forwardSpeedLimit = 0.8f;
        else if (Input.GetKeyUp(KeyCode.Alpha9))
            forwardSpeedLimit = 0.9f;
        else if (Input.GetKeyUp(KeyCode.Alpha0))
            forwardSpeedLimit = 1.0f;
        //END ANALOG ON KEYBOARD DEMO CODE  


        //do some filtering of our input as well as clamp to a speed limit
        filteredForwardInput = Mathf.Clamp(Mathf.Lerp(filteredForwardInput, v,
            Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);

        filteredTurnInput = Mathf.Lerp(filteredTurnInput, h,
            Time.deltaTime * turnInputFilter);

        Forward = filteredForwardInput;
        Turn = filteredTurnInput;
        RotationY = rotationY;
        RotationX = rotationX;





        //Capture "fire" button for action event
        Fire = Input.GetButtonDown("Fire1");
        Cease = Input.GetButtonUp("Fire1");

        Jump = Input.GetButton("Jump");

        Crouch = Input.GetButtonDown("Crouch");

        TurnHead = Input.GetButtonDown("TurnHead");

        Toggle = Input.GetButtonDown("Toggle");

    }

    //[System.Obsolete]
    //public bool IsInPivot()
    //{
    //    return stateInfo.nameHash == m_LocomotionPivotLId ||
    //        stateInfo.nameHash == m_LocomotionPivotRId ||
    //        transInfo.nameHash == m_LocomotionPivotLTransId ||
    //        transInfo.nameHash == m_LocomotionPivotRTransId;
    //}

    //public bool IsInLocomotion()
    //{
    //    return stateInfo.fullPathHash == m_LocomotionId;
    //}

    //public void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut, ref float angleOut, bool isPivoting)
    //{
    //    Vector3 rootDirection = root.forward;

    //    Vector3 stickDirection = new Vector3(Forward, 0, Turn);

    //    speedOut = stickDirection.sqrMagnitude;

    //    // Get camera rotation
    //    Vector3 CameraDirection = camera.forward;
    //    CameraDirection.y = 0.0f; // kill Y
    //    Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, Vector3.Normalize(CameraDirection));

    //    // Convert joystick input in Worldspace coordinates
    //    Vector3 moveDirection = referentialShift * stickDirection;
    //    Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

    //    Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), moveDirection, Color.green);
    //    Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.magenta);
    //    Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), stickDirection, Color.blue);
    //    Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2.5f, root.position.z), axisSign, Color.red);

    //    float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
    //    if (!isPivoting)
    //    {
    //        angleOut = angleRootToMove;
    //    }
    //    angleRootToMove /= 180f;

    //    directionOut = angleRootToMove * directionSpeed;
    //}
}
