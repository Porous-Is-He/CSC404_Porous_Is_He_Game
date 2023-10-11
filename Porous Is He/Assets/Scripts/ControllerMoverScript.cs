using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script is responsible for the player movment with a controller.
public class ControllerMoverScript : MonoBehaviour
{
    private PlayerInputActions controls;
    private Vector2 playerVelocity;


    void Awake()
    {
        controls = new PlayerInputActions();

        controls.Player.Movement.performed += ctx => playerVelocity = ctx.ReadValue<Vector2>();
        controls.Player.Movement.performed += ctx => playerVelocity = Vector2.zero;
    }

    void Update()
    {
        Vector3 move = new Vector3(-playerVelocity.x, 0, playerVelocity.y) * Time.deltaTime;
        transform.Translate(move, Space.World);
    }

    void onEnable()
    {
        controls.Player.Enable();
    }

    void onDisable()
    {
        controls.Player.Disable();
    }

}
