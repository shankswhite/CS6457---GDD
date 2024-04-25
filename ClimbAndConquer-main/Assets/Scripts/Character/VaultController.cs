using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultController : MonoBehaviour
{
    GroundScanner scanner;
    private CharacterInput cinput;
    private Animator anim;
    private CharacterScriptMotionController playerController;
    private Rigidbody rbody;
    private CapsuleCollider clder;
    private CharacterAudioHandler audioPlayer;
    [SerializeField] GameObject staminaBar;
    private Stamina stamina;

    private bool isJump;
    private int groundContactCount = 0;
    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;


    [SerializeField] List<VaultToggle> valutOptions;


    private void Awake()
    {
        scanner = GetComponent<GroundScanner>();
        cinput = GetComponent<CharacterInput>();
        anim = GetComponent<Animator>();
        playerController = GetComponent<CharacterScriptMotionController>();
        rbody = GetComponent<Rigidbody>();
        clder = GetComponent<CapsuleCollider>();
        audioPlayer = GetComponent<CharacterAudioHandler>();
        stamina = staminaBar.GetComponent<Stamina>();


    }

    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }

    private void Update()
    {
        if (cinput.Jump && !isJump)
        {
            var hitData = scanner.ObstacleCheck();
            if (hitData.forwardHitFound)
            {
                foreach (var action in valutOptions)
                {
                    if (action.CheckJumpable(hitData, transform))
                    {
                        StartCoroutine(DoJump(action));
                        
                        break;
                    }
                }
            }
            else
            {
                bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);
                if (isGrounded && !isJump)
                {
                    //audioPlayer.PlayJumpSound();
                    anim.CrossFade("Jump", 0.2f);
                    audioPlayer.EnableMoveSound();
                    audioPlayer.PlayJumpSound();

                }

            }
        }

    }


    IEnumerator DoJump(VaultToggle action)
    {
        isJump = true;
        playerController.SetControl(true);
        rbody.useGravity = false;
        clder.enabled = false;
        //rbody.isKinematic = true;
        anim.CrossFade(action.AnimName, 0.2f);
        
        stamina.takeDamage();
        yield return null;

        var animState = anim.GetNextAnimatorStateInfo(0);
        //Debug.Log("test" + action.AnimName + animState.nameHash);


        if (!animState.IsName(action.AnimName))
        {
            //Debug.Log("test again" + action.AnimName + animState.nameHash);
        }
        

        float timer = 0f;
        while (timer <= animState.length)
        {
            timer += Time.deltaTime;
            if (action.RotateToObstacle)
            {
                //TODO: rotate failed.
                //Debug.Log("before" + transform.rotation);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, action.TargetRotation, playerController.RotationSpeed * Time.deltaTime);
                //Debug.Log("after" + transform.rotation);
            }

            if (action.EnableTargetMatching)
            {
                //Debug.Log("run?");

                MatchTarget(action);

            }
            if (anim.IsInTransition(0) && timer > 0.5f)
                break;
            yield return null;
        }

        playerController.SetControl(false);
        rbody.useGravity = true;
        clder.enabled = true;
        //rbody.isKinematic = false;
        isJump = false;
        audioPlayer.StopMoveSound();
    }

    void MatchTarget(VaultToggle action)
    {
        if (anim.isMatchingTarget)
        {
            return;
        }
        anim.MatchTarget(action.MatchPos, transform.rotation, action.MatchBodyPart, new MatchTargetWeightMask(new Vector3(0, 1, 1), 0), action.MatchStartTime, action.MatchMatchTime);
        //Debug.Log(anim.name);
    }
}
