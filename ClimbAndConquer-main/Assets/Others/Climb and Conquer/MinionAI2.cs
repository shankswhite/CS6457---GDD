using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
//using UnityEditor.Animations;
//using Unity

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class MinionAI2 : MonoBehaviour

    
{
    // Start is called before the first frame update
    public int currWaypoint;
    public GameObject[] waypoints;
    public GameObject[] movingWaypoints;
    public Animator minController;
    public bool minionAIState = false;
    NavMeshAgent minionMeshAgent;

    void Awake()
    {
        //minionAnimatorScript = gameObject.GetComponent<MinionNoRootAnimController>();
        //var path = AssetDatabase.GetAssetPath(this);
        //var animatorController = AssetDatabase.LoadMainAssetAtPath(path) as AnimatorController;
        //var minController = GameObject.GetComponent<MinionNoRootAnimController>();
        minController = GetComponent<Animator>();

    }
    void Start()
    {
        currWaypoint = -1;
        minionMeshAgent = GetComponent<NavMeshAgent>();
        setNextWaypoint();
        
    }

    // Update is called once per frame
    void Update()
    {
        //might need to change from 0 to something very small
        if (minionMeshAgent.remainingDistance <= 0.01 && minionMeshAgent.pathPending == false) {
            setNextWaypoint();
        }

        minController.SetFloat("vely", minionMeshAgent.velocity.magnitude / minionMeshAgent.speed);
    }

    private void setNextWaypoint()
    {
        if (minionAIState == false)
        {
            if (waypoints.Length > 0)
            {
                currWaypoint++;
                //might need to make this >
                if (currWaypoint >= waypoints.Length)
                {
                    currWaypoint = 0;
                    minionAIState=true;
                }
                //This is updatiung the mesh.
                minionMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);
            }
        }
        else
        {
            minionMeshAgent.SetDestination(movingWaypoints[0].transform.position);
            minionAIState = false;
        }

    }
}
