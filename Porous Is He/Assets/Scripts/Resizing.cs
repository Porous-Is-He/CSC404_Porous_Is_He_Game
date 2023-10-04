using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizing : MonoBehaviour
{
    
    private LiquidTracker _liquidTracker;

    private Vector3 originalScale;
    private Vector3 smallScale;
    private Vector3 mediumScale;
    private Vector3 largeScale;
    private Vector3 diff = new Vector3(0.15f, 0.15f, 0.15f);
       

    void Start()
    {
        _liquidTracker = gameObject.GetComponent<LiquidTracker>();
        originalScale = transform.localScale;
        smallScale = originalScale + diff;
        mediumScale = smallScale + diff;
        largeScale = mediumScale + diff;
    }

    // Update is called once per frame
    void Update()
    {
        int amount = _liquidTracker.GetSelectedLiquid().liquidAmount;

        if (amount == 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, 2 * Time.deltaTime);
        } else if (amount == 1)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, smallScale, 2 * Time.deltaTime);
        } else if (amount == 2)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, mediumScale, 2 * Time.deltaTime);
        } else if (amount == 3)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, largeScale, 2 * Time.deltaTime);
        }

    }
}
