using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSource : MonoBehaviour
{

    public string liquidType;
    public int maxLiquid;

    private LiquidInfo liquid;
    private const int INFINITE = -1;

    private Vector3 originalScale;



    void Start()
    {
        liquid = new LiquidInfo(liquidType, maxLiquid);
        originalScale = transform.localScale;
     
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            
            AbsorbLiquid(2);

        }
    }

    public LiquidInfo AbsorbLiquid(int amount)
    {
        if (maxLiquid == INFINITE) return liquid;


        if (liquid.liquidAmount - amount <= 0)
        {
            liquid.liquidAmount = 0;
            Destroy(gameObject);
        } else
        {
            liquid.liquidAmount -= amount;
            float liquidRatio = (float)liquid.liquidAmount/(float)maxLiquid;
            Vector3 newScale = new Vector3(originalScale.x * liquidRatio, originalScale.y * liquidRatio, originalScale.z * liquidRatio);
            transform.localScale = newScale;

        }

        return liquid;
    }
}
