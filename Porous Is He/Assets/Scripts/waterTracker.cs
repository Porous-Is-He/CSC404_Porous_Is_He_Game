using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterTracker : MonoBehaviour
{
    public Text waterAmountText;
    private static int waterAmount;
    public static int maxWaterAmount = 3;

    // Start is called before the first frame update
    void Start()
    {
        waterAmount = 0;
        
        waterAmountText.text = "Water Amount: " + waterAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if(waterAmount > maxWaterAmount)
        {
            waterAmount = maxWaterAmount;
        }
        UpdateWaterAmountText();


    }

    private void UpdateWaterAmountText()
    {
        waterAmountText.text = "Water Amount: " + waterAmount.ToString();
    }

    public void SetWaterAmount(int amount)
    {
        waterAmount = amount;
        
    }

    public void DecrementWaterAmount()
    {
        waterAmount--;
        
    }

    public int GetWaterAmount()
    {
        return waterAmount;
    }



}
