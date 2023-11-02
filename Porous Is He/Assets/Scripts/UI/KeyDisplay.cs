using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDisplay : MonoBehaviour
{
    [Header("Toggles")]
    [SerializeField] private Toggle showControls;

    [Header("Scripts")]
    [SerializeField] private ShootingScript shootingScript;
    [SerializeField] private LiquidTracker liquidTracker;

    [Header("UI elements")]
    [SerializeField] private GameObject aimingDisplay;
    [SerializeField] private GameObject keyDisplay;
    [SerializeField] private GameObject swapKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showControls.isOn)
        {
            if (shootingScript.aiming)
            {
                aimingDisplay.SetActive(true);
                keyDisplay.SetActive(false);
            }
            else
            {
                aimingDisplay.SetActive(false);
                keyDisplay.SetActive(true);
                if (liquidTracker.maxLiquidType > 1)
                {
                    swapKey.SetActive(true);
                } else
                {
                    swapKey.SetActive(false);
                }
            }
        } else
        {
            aimingDisplay.SetActive(false);
            keyDisplay.SetActive(false);
        }
    }
}
