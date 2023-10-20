using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbsorbLiquid : MonoBehaviour
{
    // this script is attached to Player. It provides the absorb mechanic
    public int amountAbsorbed;
    private bool touchingLiquid = false;
    private PlayerInputActions playerInputActions;
    private LiquidSource liquidSource;
    private LiquidTracker liquidTracker;

    public GameObject interactUI;

    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Absorb.started += Absorb;

        liquidTracker = GetComponent<LiquidTracker>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        interactUI.SetActive(false);
        touchingLiquid = false;

        if (hit.collider.gameObject.tag == "Water")
        {
            interactUI.SetActive(true);
            touchingLiquid = true;
            liquidSource = hit.collider.gameObject.GetComponent<LiquidSource>();
        }
    }

    private void Absorb(InputAction.CallbackContext context)
    {
        if (touchingLiquid && !liquidTracker.FullLiquid())
        {
            LiquidInfo liquid = liquidSource.AbsorbLiquid(amountAbsorbed);
            if (liquid.liquidAmount != 0)
            {
                gameObject.GetComponent<PoSoundManager>().PlaySound("Absorb");
                gameObject.GetComponent<LiquidTracker>().AddSelectedLiquid(liquid);
            }
        }
    }
}
