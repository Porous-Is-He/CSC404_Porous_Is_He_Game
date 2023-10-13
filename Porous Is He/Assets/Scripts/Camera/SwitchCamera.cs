using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


// This Switches the camera from 3rd person view to aiming mode
public class SwitchCamera : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineVirtualCamera aimCamera;
    private Transform projectile;
    public GameObject crosshair;

    private bool aiming = false;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();

    public float speedV = 0.001f;
    public float speedH = 0.001f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private PlayerInputActions playerInputActions;

    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Aim.started += Aim;
        playerInputActions.Player.Aim.canceled += StopAim;

        // initial setup
        thirdPersonCamera.Priority = 20;
        aimCamera.Priority = 10;
        crosshair.SetActive(false);
        thirdPersonCamera.m_RecenterToTargetHeading.m_enabled = false;

        projectile = transform.Find("ProjectileSpawn");
    }

    void Update()
    {
        if (aiming)
        {
            AimCamera();
        }
    }

    private void Aim(InputAction.CallbackContext context)
    {
        // Get the center where the camera is pointing at
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Vector3 mouseWorldPosition = ray.GetPoint(50);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        // Rotate the player to face the camera's aim
        Vector3 worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        transform.forward = aimDirection;

        // Set player's curent rotation so that we can adjust this in AimCamera()
        rotationX = transform.eulerAngles.x;
        rotationY = transform.eulerAngles.y;

        // Enable third person camera recenter so that when we switch back to default camera,
        // the camera will be behind the player.
        thirdPersonCamera.m_RecenterToTargetHeading.m_enabled = true;

        // Switch to aiming mode
        aiming = true;
        thirdPersonCamera.Priority = 10;
        aimCamera.Priority = 20;
        crosshair.SetActive(true);

        // Disable movement, enable shooting
        gameObject.GetComponent<MoverScript>().aiming = true;
        projectile.GetComponent<ShootingScript>().aiming = true;
    }

    private void StopAim(InputAction.CallbackContext context)
    {
        // Reset player's x rotation
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

        // Switch to default camera mode
        aiming = false;
        thirdPersonCamera.Priority = 20;
        aimCamera.Priority = 10;
        crosshair.SetActive(false);

        // Enable movement, disable shooting
        gameObject.GetComponent<MoverScript>().aiming = false;
        projectile.GetComponent<ShootingScript>().aiming = false;

        // Disable automatic recenter
        thirdPersonCamera.m_RecenterToTargetHeading.m_enabled = false;
    }

    private void AimCamera()
    {

        Vector2 inputVector = playerInputActions.Player.MouseLook.ReadValue<Vector2>();
        rotationX -= speedV * inputVector.y;
        rotationY += speedH * inputVector.x;

        if (rotationX > 28.0f)
        {
            rotationX = 28.0f;
        }
        else if (rotationX < -16.0f)
        {
            rotationX = -16.0f;
        }

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}
