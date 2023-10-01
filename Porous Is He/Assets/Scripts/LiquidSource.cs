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



    void Start()
    {
        liquid = new LiquidInfo(liquidType, maxLiquid);
    }

    void Update()
    {
        
    }

    public LiquidInfo AbsorbLiquid(int amount)
    {
        if (maxLiquid == INFINITE) return liquid;

        liquid.liquidAmount -= amount;


        if (liquid.liquidAmount - amount <= 0)
        {
            liquid.liquidAmount = 0;
        } else
        {
            liquid.liquidAmount -= amount;
        }

        return liquid;
    }
}
