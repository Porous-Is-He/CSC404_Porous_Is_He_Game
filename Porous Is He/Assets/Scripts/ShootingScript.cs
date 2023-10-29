using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class ShootingScript : MonoBehaviour
{

    public bool aiming = false;
    private GameObject Player;
    private LiquidTracker liquidTracker;

    // Shooting variables
    [SerializeField] private float shootForce = 15;
    [SerializeField] private float shootUpwardForce = 10;
    [SerializeField] private float shootDistance = 60;
    [SerializeField] private float shootAmount = 0.025f;
    private bool shooting = false;

    private PlayerInputActions playerInputActions;

    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Shoot.started += StartShoot;
        playerInputActions.Player.Shoot.canceled += StopShoot;

        Player = GameObject.Find("Player");
        liquidTracker = Player.GetComponent<LiquidTracker>();
    }

    private void StartShoot(InputAction.CallbackContext context)
    {
        if (PauseMenu.isPaused) return;
        shooting = true;
    }

    private void StopShoot(InputAction.CallbackContext context)
    {
        shooting = false;
    }

    private void Update()
    {
        if (shooting && CanShoot())
        {
            CreateBullet();
            DeductLiquid();
        }
    }

    private bool CanShoot()
    {
        LiquidInfo Liquid = liquidTracker.GetSelectedLiquid();
        if (PoCombust.isOnFire) return false;
        if (Liquid.liquidAmount <= 0)
            return false;
        if (!aiming) return false;

        return true;
    }
    private void DeductLiquid()
    {
        liquidTracker.RemoveSelectedLiquid(shootAmount);
    }
    private void CreateBullet()
    {
        LiquidInfo Liquid = liquidTracker.GetSelectedLiquid();

        Vector3 attackPointPos = transform.position;

        // Find hit position (at center of screen)
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        Vector3 targetPoint = ray.GetPoint(shootDistance);
        Vector3 direction = targetPoint - attackPointPos;

        // Spawn bullet
        //GameObject currentBullet = Instantiate(bullet, attackPointPos, Quaternion.identity);
        GameObject currentBullet = transform.GetComponent<ProjectileFactory>().NewProjectile(Liquid, attackPointPos, Quaternion.identity);
        currentBullet.transform.forward = direction.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce((direction.normalized * shootForce) + (Vector3.up * shootUpwardForce), ForceMode.Impulse);
        //PoSoundManager playerSound = Player.GetComponent<PoSoundManager>();
        //playerSound.PlaySound("Shoot");

    }

    public Vector3 GetVelocity()
    {
        Vector3 attackPointPos = transform.position;

        // Find hit position (at center of screen)
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        Vector3 targetPoint = ray.GetPoint(shootDistance);
        Vector3 direction = targetPoint - attackPointPos;
        Vector3 up = Vector3.up * shootUpwardForce;
        return (direction.normalized * shootForce) + up;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Disable();
    }


    // THis is the old shooting script. Keeping it in case we need it.

    /*    public float shootForce;
        [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
        public bool aiming = false;

        private PlayerInputActions playerInputActions;

        void Start()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Shoot.started += Shoot;
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            if (PauseMenu.isPaused) return;
            if (!CanShoot())return;
            CreateBullet();
            DeductLiquid();
        }

        private bool CanShoot()
        {
            GameObject Player = GameObject.Find("Player");
            LiquidTracker LiqTrack = Player.GetComponent<LiquidTracker>();
            LiquidInfo Liquid = LiqTrack.GetSelectedLiquid();
            if (PoCombust.isOnFire) return false;
            if (Liquid.liquidAmount == 0)
                return false;
            if (!aiming) return false;

            return true;
        }
        private void DeductLiquid()
        {
            GameObject Player = GameObject.Find("Player");
            LiquidTracker LiqTrack = Player.GetComponent<LiquidTracker>();
            LiqTrack.RemoveSelectedLiquid(1);
        }
        private void CreateBullet()
        {
            GameObject Player = GameObject.Find("Player");
            LiquidTracker LiqTrack = Player.GetComponent<LiquidTracker>();
            LiquidInfo Liquid = LiqTrack.GetSelectedLiquid();

            Vector3 attackPointPos = transform.position;

            // Find hit position (at center of screen)
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

            // If ray hit something, aim there
            Vector3 targetPoint = ray.GetPoint(50);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                targetPoint = raycastHit.point;
            }

            // Calculate direction from attackPoint to targetPoint
            Vector3 direction = targetPoint - attackPointPos;

            // Spawn bullet
            //GameObject currentBullet = Instantiate(bullet, attackPointPos, Quaternion.identity);
            GameObject currentBullet = transform.GetComponent<ProjectileFactory>().NewProjectile(Liquid, attackPointPos, Quaternion.identity);
            currentBullet.transform.forward = direction.normalized;
            currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

            PoSoundManager playerSound = Player.GetComponent<PoSoundManager>();
            playerSound.PlaySound("Shoot");

        }

        private void OnDestroy()
        {
            playerInputActions.Player.Disable();
        }*/
}
