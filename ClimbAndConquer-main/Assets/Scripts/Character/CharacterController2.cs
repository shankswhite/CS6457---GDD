using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class CharacterController2 : MonoBehaviour
{
    private CharacterInput cinput;
    float _inputForward = 0f;
    float _inputTurn = 0f;
    bool _inputArmed;
    bool _inputFire;

    private Rigidbody rbody;

    private Animator anim;

    private CharacterController characterController;

    private CharacterAudioHandler audioPlayer;

    public float moveSpeed = 5f;
    public float moveSpeedBoost = 1f;
    [SerializeField] float rotationSpeed = 5f;

    // Fall speed
    private float ySpeed;




    //private bool isBoosted = false;
    //[SerializeField] float panBackSpeedPara = 1f;

    [SerializeField] Vector2 lookDirection;
    [SerializeField] Vector3 lastLookDirection;
    [SerializeField] Transform spine;
    [SerializeField] float spineRotationSpeed;
    private Transform cameraTransform;

    private Quaternion targetRotation;
    //private Quaternion aimTargetRotation;
    //private float verticalLookRotation;


    private int groundContactCount = 0;

    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;

    private bool isRootMotion;

    [SerializeField] Transform weapon;
    [SerializeField] Transform hand;
    [SerializeField] Transform hip;
    [SerializeField] GameObject laser;

    [SerializeField] GameObject staminaBar;
    //private stamina stamina;

    //private float _inputdirx;
    //private float _inputdiry;

    public bool isWin;


    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }


    void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        cinput = GetComponent<CharacterInput>();
        cameraTransform = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        audioPlayer = GetComponent<CharacterAudioHandler>();
        //stamina = staminaBar.GetComponent<Stamina>();

    }


    private void Update() 
    {
        _inputForward = cinput.Forward;
        _inputTurn = cinput.Turn;

        float moveAmount = Mathf.Abs(_inputForward) + Mathf.Abs(_inputForward);
        Vector3 moveInput = (new Vector3(_inputTurn, 0, _inputForward)).normalized;

        

        var cameraRotation = Quaternion.Euler(0, cameraTransform.rotation.y * 100f, 0);
        Debug.Log(cameraRotation);

        Vector3 moveDir = cameraRotation * moveInput;

        bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);


        if (isGrounded)
        {
            ySpeed = -0.5f;
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (moveAmount > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
            
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


    }
    private void LateUpdate()
    {
        Vector3 moveInput = (new Vector3(_inputTurn, 0, _inputForward)).normalized;
        rbody.MovePosition(rbody.position + moveInput * moveSpeed * Time.deltaTime);
    }




}
