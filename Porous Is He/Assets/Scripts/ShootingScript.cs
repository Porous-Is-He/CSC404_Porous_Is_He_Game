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

    public GameObject bullet;
    public float shootForce;
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
    }
}
