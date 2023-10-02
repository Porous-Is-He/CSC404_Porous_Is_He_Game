using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSource : MonoBehaviour
{
    //this script is attached to a liquid source
    public string liquidType;
    public int maxLiquid;
    public int amountAbsorbed;

    private LiquidInfo liquid;
    private const int INFINITE = -1;

    private Vector3 originalScale;
    private Vector3 newScale;



    void Start()
    {
        liquid = new LiquidInfo(liquidType, maxLiquid);
        originalScale = transform.localScale;
        newScale = originalScale;
    }

    void Update()
    {
        // rescale the liquid puddle if it is not infinite
        if (maxLiquid != INFINITE)
        {
            float liquidRatio = (float)liquid.liquidAmount / (float)maxLiquid;
            newScale = new Vector3(originalScale.x * liquidRatio, originalScale.y * liquidRatio, originalScale.z * liquidRatio);
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, 2 * Time.deltaTime);

            //destroy if liquid amount becomes 0
            if (transform.localScale == Vector3.zero)
            {
                Destroy(gameObject);
            }
        }
    }


    public LiquidInfo AbsorbLiquid(int amount)
    {
        if (maxLiquid == INFINITE) return liquid;

        if (liquid.liquidAmount - amount <= 0)
        {
            liquid.liquidAmount = 0;
        }
        else
        {
            liquid.liquidAmount -= amount;
        }
        return liquid;
    }

}
