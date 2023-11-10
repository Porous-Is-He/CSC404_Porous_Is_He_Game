using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(CharacterController))]

public class MoverScript : MonoBehaviour
{
    public CharacterController controller;
    private PlayerInputActions playerInputActions;
    private Transform cameraMainTransform;

    private static float verticalMod;

    // Jump variables
    private float playerVelocity;
    private float gravityValue;
    private float jumpPower;
    private int numberOfJumps = 0;
    private int maxNumberOfJumps = 2;

    // Player Movement variables
    private Vector2 inputVector;
    private Vector3 direction;
    private float playerSpeed = 7.5f;
    private float turnSmoothTime = 0.05f;
    private float turnSmoothVelocity;

    // Variables that deals with knockback
    public float knockBackForce = 4f;
    public float knockBackForceHorizontal = 2f;
    public float knockBackTime = 0.1f;
    private float knockBackCounter;

    // Variable that toggles movement
    private bool enableMovement = true;

    // Variable that deals with aiming
    public bool aiming = false;

    private Vector3 hitNormal;
    public bool isGrounded_Custom;
    private float slopeLimit;
    public float slideSpeed = 1.0f;

    // used in seesaw script
    public bool onMovingPlatform;
    private bool isJumping = false;

    private DifficultyManager diffm;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.started += Jump;
    }
    private void Start()
    {
        cameraMainTransform = Camera.main.transform;
        slopeLimit = controller.slopeLimit;

        //AssignMovement();
        diffm = GameObject.Find("GameController").GetComponent<DifficultyManager>();
        diffm.loadedEvent.AddListener(AssignMovement);
    }

    private void AssignMovement()
    {
        float jumpMulti = 1;
        if (diffm.difficultyAssigned)
        {
            if (diffm.isHardmode)
            {
                verticalMod = 0.9f;
            } else
            {
                jumpMulti = 1.05f;
                verticalMod = 0.75f;
            }
        } else
        {
            verticalMod = 0.9f;
        }

        gravityValue = -8.8f * verticalMod;
        jumpPower = 2.5f * verticalMod * jumpMulti;
    }


    void Update()
    {
        Vector3 slidingMovement = new Vector3();

        //grounded detection
        if (!isGrounded_Custom)
        {
            slidingMovement.x += (1f - hitNormal.y) * hitNormal.x * slideSpeed;
            slidingMovement.z += (1f - hitNormal.y) * hitNormal.z * slideSpeed;
        }
        if ( !(Vector3.Angle(Vector3.up, hitNormal) <= slopeLimit) )
        {
            isGrounded_Custom = false;
        }
        else
        {
            isGrounded_Custom = controller.isGrounded;
        }



        if (LevelComplete.LevelEnd) return;

        if (knockBackCounter <= 0)
        {
            if (!aiming && enableMovement)
            {
                Move();
                ApplyGravity();
                controller.Move((direction * playerSpeed + slidingMovement) * Time.deltaTime);
            }
            else
            {
                MoveWhileAiming();
                controller.Move((direction * playerSpeed + slidingMovement) * Time.deltaTime);
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            ApplyGravity();
            controller.Move(direction * playerSpeed * Time.deltaTime);
            if (IsGrounded())
            {
                direction.x = 0.0f;
                direction.z = 0.0f;
            }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }



    private void MoveWhileAiming()
    {
        // To be implemented
        if (IsGrounded() && playerVelocity < 0f)
        {
            playerVelocity = 0.0f;
            direction.x = 0f;
            direction.z = 0f;
        }
        else
        {
            playerVelocity += gravityValue * Time.deltaTime;
        }

        direction.y = playerVelocity;
    }

    private void Move()
    {
        inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        direction = new Vector3(inputVector.x, 0f, inputVector.y);
        direction = cameraMainTransform.forward * direction.z + cameraMainTransform.right * direction.x;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    private void ApplyGravity()
    {
        if (onMovingPlatform && !isJumping)
            playerVelocity = gravityValue;
        else if (IsGrounded() && playerVelocity < 0f)
            playerVelocity = 0.0f;
        else 
            playerVelocity += gravityValue * Time.deltaTime;

        direction.y = playerVelocity;
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (!enableMovement) return;
        if ((!IsGrounded() && numberOfJumps >= maxNumberOfJumps) || aiming) return;
        isJumping = true;
        if (numberOfJumps == 0) StartCoroutine(WaitForLanding());


        float playerWeight = GameObject.Find("Player").GetComponent<LiquidTracker>().CalcWeight();
        if (playerWeight > 0)
        {
            playerVelocity = jumpPower * (8.0f / 10.0f);
        }
        else
        {
            playerVelocity = jumpPower;
        }

        numberOfJumps++;
    }

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);
        numberOfJumps = 0;
        isJumping = false;
    }

    //private bool IsGrounded() => controller.isGrounded;
    private bool IsGrounded() => isGrounded_Custom;

    public void KnockBack(Vector3 moveDirection)
    {
        //Debug.Log("PUSHHH"); // Nice little debug statement to check stuff

        knockBackCounter = knockBackTime;
        //enableMovement = false;
        //WaitForLanding();
        direction = moveDirection * knockBackForceHorizontal;
        direction.y = knockBackForce;
        playerVelocity = knockBackForce;

        /*        if (knockBackCounter >= 0)
                {
                    enableMovement = true;
                }*/
    }
    private void OnDestroy()
    {
        playerInputActions.Player.Disable();
    }
}
