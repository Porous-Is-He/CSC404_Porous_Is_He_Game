using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidTracker : MonoBehaviour
{
    private LiquidInfo[] playerLiquids;

    // current liquid index
    private int liquidSelectionIndex = 0;

    // max number of liquid types player is allowed to have
    public int maxLiquidType = 2;

    public int maxLiquidAmount = 3;

    public Rigidbody rb;

    public float normalMass = 20f;
    public float heavyMass = 40f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = normalMass;
        playerLiquids = new LiquidInfo[maxLiquidType];
        for (int i = 0; i < maxLiquidType; ++i)
        {
            if (i == 0)
                playerLiquids[i] = new LiquidInfo("Water", 0);
            else
                playerLiquids[i] = new LiquidInfo("None", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CalcWeight().Equals(maxLiquidAmount))
        {
            rb.mass = heavyMass;
        }
        //Debug.Log(rb.mass.ToString());
    }

    public LiquidInfo GetSelectedLiquid()
    {
        return playerLiquids[liquidSelectionIndex];
        
    }

    public int AddSelectedLiquid(LiquidInfo liquid)
    {
        // if liquid is the same as player's current liquid
        if (liquid.liquidType.Equals(playerLiquids[liquidSelectionIndex].liquidType))
        {
            int beforeAmount = playerLiquids[liquidSelectionIndex].liquidAmount;
            int tempAmount = playerLiquids[liquidSelectionIndex].liquidAmount + liquid.liquidAmount;
            playerLiquids[liquidSelectionIndex].liquidAmount = (tempAmount > maxLiquidAmount) ? maxLiquidAmount : tempAmount;

            return playerLiquids[liquidSelectionIndex].liquidAmount - beforeAmount;
        }
        else
        {
            bool liquidAdded = false;
            int amountLiquidAdded = 0;
            for(int i = 0; i < maxLiquidType; i++)
            {
                
                if(playerLiquids[i] == null && !liquidAdded)
                {
                    playerLiquids[i] = liquid;

                    amountLiquidAdded = liquid.liquidAmount;

                    liquidAdded = true;

                    liquidSelectionIndex = i;
                }
                else if(playerLiquids[i] != null)
                {
                    // clear non-current liquid
                    playerLiquids[i].liquidAmount = 0;

                }
            }

            return amountLiquidAdded;
        }
        
    }

    public void RemoveSelectedLiquid(int amount)
    {
        playerLiquids[liquidSelectionIndex].liquidAmount -= amount;
    }

    public int GetSelectionIndex()
    {
        return liquidSelectionIndex;
    }

    public void SetSelectionIndex(int index)
    {
        liquidSelectionIndex = index;
    }

    public int CalcWeight()
    {
        int weight = 0;
        for(int i = 0; i < maxLiquidType; i++)
        {
            if(playerLiquids[i] != null)
            {
                weight += playerLiquids[i].liquidAmount;
            }
        }

        

        return Mathf.Min(weight, maxLiquidAmount);
    }
}
