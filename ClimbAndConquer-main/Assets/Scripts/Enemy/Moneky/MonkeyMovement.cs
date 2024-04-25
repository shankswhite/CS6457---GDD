using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyMovement : MonoBehaviour
{

    
    private Animator animator;
    public GameObject ammoPrefab;
    private GameObject playerObject;
    private GameObject ammoObject;
    private MonkeyState currentState;
    private MonkeyState pendingState;
    private Vector3 nextPosition;
    private int nextDirection = 1;
    private LineRenderer lineRenderer;
    private MonkeyAudioHandler monkeyAudioHandler;



    enum MonkeyState
    {
        ATTACKING,
        MOVING,
        TAUNTING,
        IDLE_STAND,
        IDLE_SIT,
        DYING
    }

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.playerObject = GameObject.FindGameObjectWithTag("Player");
        monkeyAudioHandler = GetComponent<MonkeyAudioHandler>();

        SetState(MonkeyState.ATTACKING);

        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Set the width of the Line Renderer
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // Set the number of vertex counts
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == MonkeyState.IDLE_SIT)
        {
            // hasn't seen player yet. Need to come close to trigger their observation.
            if (DistanceToPlayer() < 12f)
            {
                seenEnemy();
            }

        } else
        {
            // has seen the player. Player needs to go pretty far away to avoid their gaze.
            if (DistanceToPlayer() > 20f)
            {
                SetState(MonkeyState.IDLE_SIT);
            }
        }
        
        
        if (currentState == MonkeyState.ATTACKING)
        {
            gameObject.transform.LookAt(playerObject.transform.position);
            Transform handTransform = gameObject.transform.Find("Armature")
                .Find("Hips")
                .Find("Spine")
                .Find("Spine1")
                .Find("Spine2")
                .Find("RightShoulder")
                .Find("RightArm")
                .Find("RightForeArm")
                .Find("RightHand");
            ammoObject.transform.position = handTransform.position;
        } else if (currentState == MonkeyState.MOVING)
        {
            Vector3 currentPosition = gameObject.transform.position;
            Vector3 newPosition = Vector3.MoveTowards(currentPosition, nextPosition, 0.1f);
            gameObject.transform.position = newPosition;
            gameObject.transform.LookAt(nextPosition);

        }

    }

    public void onJumpEvent()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 5f, 0f);
        if (DistanceToPlayer() < 20f)
        {
            monkeyAudioHandler.PlayTauntSound();
        }
    }

    public void OnThrowEnded()
    {
        Vector3 playerTarget = playerObject.transform.position;
        playerTarget.y = playerTarget.y + 1.5f;
        Vector3 throwDirection = 25*((playerTarget - ammoObject.transform.position).normalized);
        //DrawLine(playerTarget, ammoObject.transform.position, Color.red);

        Debug.Log("sidtest: " + throwDirection);
       

        SetState(MonkeyState.IDLE_STAND);
       
        Rigidbody ammoRb = ammoObject.GetComponent<Rigidbody>();
        ammoRb.velocity = new Vector3(throwDirection.x, throwDirection.y, throwDirection.z);

        monkeyAudioHandler.PlayAmmoThrownSound();
    }

    public void seenEnemy()
    {
        //monkeyAudioHandler.PlayEnemyDetectedSound();
        monkeyAudioHandler.PlayTauntSound();
        SetState(MonkeyState.ATTACKING);   
    }

    public void onIdleEnded()
    {
        SetState(MonkeyState.MOVING);
    }

    public void onRunEnded()
    {
        SetState(MonkeyState.ATTACKING);
    }

    public void onKilled()
    {
        gameObject.transform.LookAt(playerObject.transform);
        SetState(MonkeyState.DYING);
    }

    void setStateAfterAnimation(MonkeyState newState)
    {
        pendingState = newState;
    }

    void SetState(MonkeyState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case MonkeyState.ATTACKING:
                //animator.res
                animator.Play("MonkeyThrow", 0, 0.0f);
                // instantiate a banana.
                ammoObject = Instantiate(ammoPrefab);
                Physics.IgnoreCollision(ammoObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
                break;
            case MonkeyState.IDLE_STAND:
                // do nothing immediately.
                break;
            case MonkeyState.IDLE_SIT:
                animator.Play("MonkeySitIdle", 0, 0.0f);
                break;
            case MonkeyState.MOVING:
                animator.Play("MonkeyRun", 0, 0.0f);
                monkeyAudioHandler.PlayRunningSound();
                nextPosition = getNextPosition();                
                break;
            case MonkeyState.DYING:
                animator.Play("DieEditable");
                monkeyAudioHandler.PlayKilledSound();
                break;
            default:
                //animator.Play("Idle");
                break;
        }
    }

    private Vector3 getNextPosition()
    {
        Vector3 currentPosition = gameObject.transform.position;
        currentPosition.x = currentPosition.x + (nextDirection)*(5f);
        nextDirection *= -1;
        return currentPosition;
    }

    public void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        // Set the start and end of the line
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        // Set the color of the line
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    private float DistanceToPlayer()
    {
        float distance = Vector3.Distance(playerObject.transform.position, gameObject.transform.position);
        return distance;
    }

}
