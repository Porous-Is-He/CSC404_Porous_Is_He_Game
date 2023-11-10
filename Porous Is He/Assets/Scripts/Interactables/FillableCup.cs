using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillableCup : MonoBehaviour
{
    [SerializeField] private Scale scale;

    [SerializeField] private GameObject waterLevel;
    [SerializeField] private GameObject oilLevel;
    [SerializeField] private GameObject colliderLevel;

    private float colliderLevelOffset = 0.0062f;

    private float minLevel = -0.0152f;
    private float minColliderLevel = -0.014f;
    private float maxLevel = 0.015f;
    private float minScale = 0.016992f;
    private float maxScale = 0.027054f;

    private float oilAmount = 0;
    private float waterAmount = 0;
    private float maxAmount = 35;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (waterAmount + oilAmount < maxAmount)
        {
            if (other.name.StartsWith("WaterProjectile"))
                AddWaterToCup();
            else if (other.name.StartsWith("OilProjectile"))
                AddOilToCup();
        }
    }

    private void AddWaterToCup()
    {
        waterAmount++;
        scale.AddWeight(2);
        UpdateLiquidLevel();
    }

    private void AddOilToCup()
    {
        oilAmount++;
        scale.AddWeight(1);
        UpdateLiquidLevel();
    }

    private void UpdateLiquidLevel()
    {
        // update water and oil level (y position)
        float waterLevelValue = Utils.ConvertRatio(0, maxAmount, waterAmount, minLevel, maxLevel);
        waterLevel.transform.localPosition = new Vector3(0, waterLevelValue, 0);
        float oilLevelValue = Utils.ConvertRatio(0, maxAmount, waterAmount + oilAmount, minLevel, maxLevel);
        oilLevel.transform.localPosition = new Vector3(0, oilLevelValue, 0);

        // update the xz scale
        float waterScaleValue = Utils.ConvertRatio(0, maxAmount, waterAmount, minScale, maxScale);
        Vector3 currWaterScale = waterLevel.transform.localScale;
        waterLevel.transform.localScale = new Vector3(waterScaleValue, currWaterScale.y, waterScaleValue);

        float oilScaleValue = Utils.ConvertRatio(0, maxAmount, waterAmount + oilAmount, minScale, maxScale);
        Vector3 currOilScale = oilLevel.transform.localScale;
        oilLevel.transform.localScale = new Vector3(oilScaleValue, currOilScale.y, oilScaleValue);

        // update the collider y position which is colliderLevelOffset lower than waterLevelPosition
        float tempLevel = oilLevelValue - colliderLevelOffset;
        tempLevel = (tempLevel < minColliderLevel) ? minColliderLevel : tempLevel;
        tempLevel = (waterAmount + oilAmount > 0) ? tempLevel : minLevel;
        colliderLevel.transform.localPosition = new Vector3(0, tempLevel, 0);
        // update collider to same scale as waterLevel
        colliderLevel.transform.localScale = new Vector3(oilScaleValue, currOilScale.y, oilScaleValue);
    }

    public LiquidInfo RemoveLiquid(float amount)
    {
        // if there is oil, remove oil first

        string type = "";
        if (oilAmount > 0)
        {
            oilAmount -= 0.1f;
            type = "Oil";
        } else if (waterAmount > 0)
        {
            waterAmount -= 0.1f;
            type = "Water";
        }
        if (waterAmount < 0)
            waterAmount = 0;
        if (oilAmount < 0)
            oilAmount = 0;

        scale.SetWeight(CalculateWeight());
        UpdateLiquidLevel();
        return new LiquidInfo(type, amount);
    }

    public string GetSurfaceLiquidType()
    {
        if (oilAmount > 0)
            return "Oil";
        return "Water";
    }

    public float GetLiquidAmount()
    {
        return oilAmount + waterAmount;
    }

    public void EmptyCup()
    {
        oilAmount = 0;
        waterAmount = 0;
        scale.ResetWeight();
        UpdateLiquidLevel();
    }

    private float CalculateWeight()
    {
        return waterAmount * 2 + oilAmount;
    }

}
