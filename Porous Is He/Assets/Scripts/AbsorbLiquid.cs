using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbsorbLiquid : MonoBehaviour
{
    // this script is attached to Player. It provides the absorb mechanic
    [SerializeField] private float amountAbsorbed = 0.025f;
    private bool touchingLiquid = false;
    private PlayerInputActions playerInputActions;
    private LiquidSource liquidSource;
    private LiquidTracker liquidTracker;

    private bool absorbing = false;

    public GameObject interactUI;

    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Absorb.started += StartAbsorb;
        playerInputActions.Player.Absorb.canceled += StopAbsorb;

        liquidTracker = GetComponent<LiquidTracker>();
    }

    private void Update()
    {
        if (absorbing)
        {
            if (touchingLiquid && !liquidTracker.FullLiquid(liquidSource.liquidType))
            {
                LiquidInfo liquid = liquidSource.AbsorbLiquid(amountAbsorbed);
                if (liquid.liquidAmount != 0)
                {
                    //gameObject.GetComponent<PoSoundManager>().PlaySound("Absorb");
                    gameObject.GetComponent<LiquidTracker>().AddSelectedLiquid(liquid);
                }
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        interactUI.SetActive(false);
        touchingLiquid = false;

        if (hit.collider.gameObject.tag == "Water" || hit.collider.gameObject.tag == "Oil")
        {
            interactUI.SetActive(true);
            //if (liquidTracker.GetSelectedLiquid().liquidType != "Water") return;
            touchingLiquid = true;
            liquidSource = hit.collider.gameObject.GetComponent<LiquidSource>();
        }
    }

    private void StartAbsorb(InputAction.CallbackContext context)
    {
        if (PauseMenu.isPaused) return;
        absorbing = true;
        if (touchingLiquid && !liquidTracker.FullLiquid(liquidSource.liquidType))
        {
            gameObject.GetComponent<PoSoundManager>().PlaySound("Absorb");
        }
    }

    private void StopAbsorb(InputAction.CallbackContext context)
    {
        absorbing = false;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Disable();
    }
}
