using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiMovement : MonoBehaviour
{
    public Transform[] waypoints; // Points between which the Yeti will patrol.
    public float speed = 2f; // Yeti Speed
    private Animator anim;
    private GameObject player;
    private int waypointIndex = 0;
    private bool isChasing = false;

    private void Start()
    {
        this.anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogWarning("Player object not found. Make sure your player GameObject has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (!player) return; // Exit if player is not found

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if the Yeti should chase the player
        bool shouldChase = distanceToPlayer < 20f && distanceToPlayer > 5f;
        if (isChasing != shouldChase)
        {
            isChasing = shouldChase;
            anim.SetBool("isChase", isChasing);
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= 5f)
        {
            // Melee();
        }
        else
        {
            Patrol();
        }
    }


    private void Patrol()
    {
        // Debug.Log("Is patrolling");
        // Transform targetWaypoint = waypoints[waypointIndex];
        // transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // // Use a threshold for comparing the positions
        // if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        // {
        //     waypointIndex = (waypointIndex + 1) % waypoints.Length;
        // }
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

}