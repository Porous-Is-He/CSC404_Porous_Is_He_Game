using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillableCup : MonoBehaviour
{
    [SerializeField] private Scale scale;

    [SerializeField] private GameObject waterLevel;
    [SerializeField] private GameObject colliderLevel;

    private float colliderLevelOffset = 0.0062f;

    private float minWaterLevel = -0.0152f;
    private float maxWaterLevel = 0.015f;
    private float minWaterScale = 0.016992f;
    private float maxWaterScale = 0.027054f;

    private float oilAmount = 0;
    private float waterAmount = 0;
    private float maxWaterAmount = 25;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("WaterProjectile"))
        {
            if (waterAmount < maxWaterAmount)
                AddWaterToCup();
        }
    }

    private void AddWaterToCup()
    {
        waterAmount++;
        scale.AddWeight(2);

        // vfx logic
        // update water level (y position)
        float waterLevelValue = Utils.ConvertRatio(0, maxWaterAmount, waterAmount, minWaterLevel, maxWaterLevel);
        waterLevel.transform.localPosition = new Vector3(0, waterLevelValue, 0);

        // update water xz scale
        float waterScaleValue = Utils.ConvertRatio(0, maxWaterAmount, waterAmount, minWaterScale, maxWaterScale);
        Vector3 currWaterScale = waterLevel.transform.localScale;
        waterLevel.transform.localScale = new Vector3(waterScaleValue, currWaterScale.y, waterScaleValue);

        // update the collider y position which is colliderLevelOffset lower than waterLevelPosition
        colliderLevel.transform.localPosition = new Vector3(0, waterLevelValue - colliderLevelOffset, 0);
        // update collider to same scale as waterLevel
        colliderLevel.transform.localScale = new Vector3(waterScaleValue, currWaterScale.y, waterScaleValue);

    }

}
