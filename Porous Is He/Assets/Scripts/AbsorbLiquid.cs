using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbLiquid : MonoBehaviour
{
    // this script is attached to Player
    public int amountAbsorbed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // if player is touching water and presses E, then absorb the liquid
        if (hit.collider.gameObject.tag == "Water" && Input.GetKeyDown(KeyCode.E))
        {
            hit.collider.gameObject.GetComponent<LiquidSource>().AbsorbLiquid(amountAbsorbed);
        }
    }
}
