using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiController : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private Rigidbody rbody;
    private Transform yetiTransform;
    [SerializeField] float attackRange = 5;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rbody = GetComponent<Rigidbody>();
        yetiTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, yetiTransform.position);
        var direction = yetiTransform.position - player.transform.position;


        if (distance <= 20)
        {
            yetiTransform.LookAt(player.transform.position);
        }



        if (distance <= 5)
        {
            anim.SetBool("isMelle", true);
        }
        else
        {
            anim.SetBool("isMelle", false);
        }

    }

    private void doMelle()
    {
        anim.SetBool("isMelle", true);
    }
}
