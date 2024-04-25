using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class CharacterScriptMotionController : MonoBehaviour
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

    //[SerializeField] CameraController thirdPersonCam;

    public float moveSpeed = 5f;
    public float moveSpeedBoost = 1f;
    [SerializeField] float rotationSpeed = 5f;
    private bool isBoosted = false;

    [SerializeField] Vector2 lookDirection;
    [SerializeField] Vector3 lastLookDirection;
    [SerializeField] Transform spine;
    [SerializeField] float spineRotationSpeed;
    [SerializeField] float panBackSpeedPara = 1f;



    private Transform cameraTransform;
    private Quaternion targetRotation;
    private Quaternion aimTargetRotation;
    private float verticalLookRotation;


    private int groundContactCount = 0;

    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float rayLength = 1f;
    [SerializeField] float distanceToGround = 1f;

    private bool isRootMotion;

    [SerializeField] Transform weapon;
    [SerializeField] Transform hand;
    [SerializeField] Transform hip;
    [SerializeField] GameObject laser;

    [SerializeField] GameObject staminaBar;
    private Stamina stamina;

    private float _inputDirX;
    private float _inputDirY;

    public bool isWin;
    private GameObject crossHairCanvas;


    public void PickupGun()
    {
        weapon.SetParent(hand);
        //weapon.localPosition = new Vector3(-0.056f, 0.066f, -0.076f);
        //weapon.localRotation = Quaternion.Euler(26.847f, 25.496f, 96.562f);
        weapon.localPosition = new Vector3(-0.063f, 0.026f, -0.076f);
        weapon.localRotation = Quaternion.Euler(-18.8f, 29.938f, 76.226f);
        weapon.localScale = new Vector3(0.015f, 0.015f, 0.015f);
        //laser.SetActive(true);
        crossHairCanvas.SetActive(true);

    }

    public void PutDownGun()
    {
        weapon.SetParent(hip);
        weapon.localPosition = new Vector3(-0.223f, -0.038f, -0.285f);
        weapon.localRotation = Quaternion.Euler(25.622f, -56.547f, 64.783f);
        weapon.localScale = new Vector3(0.015f, 0.015f, 0.015f);
        //laser.SetActive(false);
        crossHairCanvas.SetActive(false);
    }

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

        stamina = staminaBar.GetComponent<Stamina>();

        crossHairCanvas = GameObject.FindGameObjectWithTag("CrossHair");
        crossHairCanvas.SetActive(false);

    }

    private void Update()
    {
        if (cinput.enabled)
        {
            _inputForward = cinput.Forward;
            _inputTurn = cinput.Turn;
            _inputArmed = cinput.Toggle;
            _inputFire = cinput.Fire;
            _inputDirX = cinput.RotationX;
            _inputDirY = cinput.RotationY;
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 cameraRight = cameraTransform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();


            // Script Motion for character, using transform for now
            // TODO: change to rigidbody
            Vector3 moveInput = (new Vector3(_inputTurn, 0, _inputForward)).normalized;
            float targetAngle = cameraTransform.eulerAngles.y;
            targetRotation = Quaternion.Euler(0, targetAngle, 0);
            aimTargetRotation = Quaternion.Euler(0, targetAngle + 45, 0);

            Vector3 moveDir = cameraForward * _inputForward + cameraRight * _inputTurn;
            //Vector3 movePosition = moveInput * moveSpeed * Time.deltaTime;
            //transform.position += movePosition;

            if (isRootMotion)
            {
                return;
            }


            //rbody.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
            if (_inputForward > 0)
            {
                rbody.MovePosition(rbody.position + moveDir * moveSpeed * moveSpeedBoost * Time.deltaTime);
                AlignToGround();
                //characterController.Move(moveDir * moveSpeed * Time.deltaTime);
                //rbody.MovePosition(moveDir * moveSpeed * Time.deltaTime);
                //audioPlayer.PlayRunSound();
                audioPlayer.EnableMoveSound();

            }
            else
            {
                rbody.MovePosition(rbody.position + moveDir * moveSpeed * moveSpeedBoost * panBackSpeedPara * Time.deltaTime);
                AlignToGround();
                //characterController.Move(moveDir * moveSpeed * 0.2f * Time.deltaTime);
                //rbody.MovePosition(moveDir * moveSpeed * 0.2f * Time.deltaTime);
                //audioPlayer.PlayWalkSound();
                audioPlayer.DisableMoveSound();
            }


            // Call anim
            float moveBlendPara = _inputForward;
            anim.SetFloat("MoveBlendPara", moveBlendPara, 0.2f, Time.deltaTime);
            float turnBlendPara = _inputTurn;
            anim.SetFloat("TurnBlendPara", turnBlendPara, 0.2f, Time.deltaTime);

            bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);


            if (_inputArmed)
            {
                bool armedState = anim.GetBool("StrappedUp");
                anim.SetBool("StrappedUp", !armedState);
                _inputArmed = false;
            }

            if (anim.GetBool("StrappedUp"))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, aimTargetRotation, rotationSpeed * Time.deltaTime);

            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            }


            if (_inputFire && anim.GetBool("StrappedUp"))
            {
                anim.SetTrigger("Fire");
            }
            //Debug.Log(moveSpeed);



        }

    }

    private void LateUpdate()
    {
        if (anim.GetBool("StrappedUp"))
        {
            //lastLookDirection += new Vector3(-_inputDirX * spineRotationSpeed, _inputDirY * spineRotationSpeed, 0);
            //spine.Rotate(lastLookDirection.x, lastLookDirection.y, 0);
            lookDirection += new Vector2(-_inputDirX * spineRotationSpeed, _inputDirY * spineRotationSpeed);
            lookDirection.x = Mathf.Clamp(lookDirection.x, -15, 15);
            lookDirection.y = Mathf.Clamp(lookDirection.y, -15, 15);
            //verticalLookRotation = Mathf.Clamp(verticalLookRotation, -45f, 45f); // 限制旋转角度
            spine.localEulerAngles = lookDirection + new Vector2(30, 0);
            //Debug.Log(spine.localEulerAngles);
            moveSpeed = 1f;

        }
        else
        {
            if (isBoosted)
            {
                moveSpeed = 10f;
            }
            else
            {
                moveSpeed = 5f;
            }

        }
    }

    public void SetControl(bool isRootMotion)
    {
        this.isRootMotion = isRootMotion;

        if (isRootMotion)
        {
            anim.SetFloat("MoveBlendPara", 0f);
            targetRotation = transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUps") && !isBoosted)
        {
            isBoosted = true;
            //moveSpeedBoost = 10f;
            //moveSpeed = 10f;
            Destroy(other.gameObject);
            //Debug.Log("mixi");
            Invoke("ResetSpeed", 10f);
        }
        if (other.CompareTag("Enemy"))
        {

            if (other.gameObject.transform.parent != null)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
                stamina.takeDamage();
            }
            else
            {
                Destroy(other.gameObject);
                stamina.takeDamage();
            }


        }

        if (other.CompareTag("Yeti"))
        {

            if (other.gameObject.transform.parent != null)
            {
                stamina.takeDamage();
            }
            else
            {
                Destroy(other.gameObject);
                stamina.takeDamage();
            }


        }

        if (other.CompareTag("WinPoint"))
        {
            isWin = true;
            Debug.Log("win");
        }

    }


    private void ResetSpeed()
    {
        moveSpeed = 5f;
        isBoosted = false;
    }

    private void AlignToGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, rayLength, groundLayer))
        {
            Vector3 groundedPosition = new Vector3(transform.position.x, hit.point.y + distanceToGround, transform.position.z);
            rbody.MovePosition(groundedPosition);
        }
    }

    public float RotationSpeed => rotationSpeed;





}
