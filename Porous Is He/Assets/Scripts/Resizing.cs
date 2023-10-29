using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Resizing : MonoBehaviour
{
    
    private LiquidTracker _liquidTracker;

    private Vector3 originalScale;
/*    private Vector3 smallScale;
    private Vector3 mediumScale;
    private Vector3 largeScale;
    private Vector3 diff = new Vector3(0.15f, 0.15f, 0.15f);*/
    private float increaseScaleBy = 0.45f;
    private Vector3 addScale;
       

    void Start()
    {
        _liquidTracker = gameObject.GetComponent<LiquidTracker>();
        originalScale = transform.localScale;
/*        smallScale = originalScale + diff;
        mediumScale = smallScale + diff;
        largeScale = mediumScale + diff;*/
    }

    // Update is called once per frame
    void Update()
    {
        float percentage = (_liquidTracker.GetSelectedLiquid().liquidAmount / _liquidTracker.maxLiquidAmount) * increaseScaleBy;
        addScale = new Vector3(percentage, percentage, percentage);
        transform.localScale = originalScale + addScale;

/*        if (amount == 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, 2 * Time.deltaTime);
        } else if (amount <= 1)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, smallScale, 2 * Time.deltaTime);
        } else if (amount <= 2)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, mediumScale, 2 * Time.deltaTime);
        } else if (amount <= 3)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, largeScale, 2 * Time.deltaTime);
        }*/

    }
}
