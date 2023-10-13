using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(CharacterController))]

public class MoverScript : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInputActions playerInputActions;
    private Transform cameraMainTransform;

    // Jump variables
    private float playerVelocity;
    private float gravityValue = -9.81f;
    private float jumpPower = 2.5f;
    private int numberOfJumps = 0;
    private int maxNumberOfJumps = 2;

    // Player Movement variables
    private Vector2 inputVector;
    private Vector3 direction;
    private float playerSpeed = 6.0f;
    private float turnSmoothTime = 0.05f;
    private float turnSmoothVelocity;

    public bool aiming = false;


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
    }

    void Update()
    {
        if (!aiming)
        {
            Move();
            ApplyGravity();
            controller.Move(direction * playerSpeed * Time.deltaTime);
        } else
        {
            MoveWhileAiming();
            controller.Move(direction * playerSpeed * Time.deltaTime);
        }
    }

    private void MoveWhileAiming()
    {
        // To be implemented
        if (IsGrounded() && playerVelocity < 0f)
        {
            playerVelocity = 0.0f;
        }
        else
        {
            playerVelocity += gravityValue * Time.deltaTime;
        }

        direction.y = playerVelocity;
        direction.x = 0f;
        direction.z = 0f;
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
        if (IsGrounded() && playerVelocity < 0f)
        {
            playerVelocity = 0.0f;
        } else
        {
            playerVelocity += gravityValue * Time.deltaTime;
        }
        
        direction.y = playerVelocity;
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if ((!IsGrounded() && numberOfJumps >= maxNumberOfJumps) || aiming) return;
        if (numberOfJumps == 0) StartCoroutine(WaitForLanding());

        numberOfJumps++;
        playerVelocity = jumpPower;
    }

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);
        numberOfJumps = 0;
    }

    private bool IsGrounded() => controller.isGrounded;
}
