using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoCombust : MonoBehaviour
{
    [Header("Fire Indicators")]
    [SerializeField] private GameObject FireBorder;
    [SerializeField] private GameObject PoFire;
    [SerializeField] private GameObject RedFire;
    [SerializeField] private GameObject OrangeFire;
    [SerializeField] private GameObject YellowFire;

    [Header("Length of fire")]
    [SerializeField] private float secondsOnFirePerUnit;

    private LiquidTracker liquidTracker;
    private float amount;
    private float timeCounter;
    private int oilIndex = 1;
    public static bool isOnFire;

    // change size of fire
    private float scaleModifier;
    private float startValue;
    private float endValue;
    private Animator animator;

    void Start()
    {
        liquidTracker = GetComponent<LiquidTracker>();
        isOnFire = false;
        timeCounter = 0;
        FireBorder.SetActive(false);
        PoFire.SetActive(false);
        animator = FireBorder.GetComponentInChildren<Animator>();
    }

    void Update()
    {   
        if (isOnFire)
        {
            if (timeCounter >= secondsOnFirePerUnit)
            {
                liquidTracker.RemoveLiquidFromIndex(oilIndex, 1);
                amount = liquidTracker.GetLiquidAmountFromIndex(oilIndex);

                if (amount == 0) EndFire();
                StartCoroutine(LerpFireScale(amount, RedFire, false));
                StartCoroutine(LerpFireScale(amount, OrangeFire, false));
                StartCoroutine(LerpFireScale(amount, YellowFire, false));
                timeCounter = 0;

            } else
            {
                timeCounter += Time.deltaTime;
            }
        }
        
    }

    public void Combust()
    {
        if (isOnFire) return;
        if (liquidTracker.GetSelectedLiquid().liquidType != "Oil") return;
        if (liquidTracker.GetSelectedLiquid().liquidAmount <= 0) return;

        isOnFire = true;
        FireBorder.SetActive(true);
        PoFire.SetActive(true);
        RedFire.transform.localScale = Vector3.one * FireSizeModifier(liquidTracker.GetSelectedLiquid().liquidAmount);
        OrangeFire.transform.localScale = Vector3.one * FireSizeModifier(liquidTracker.GetSelectedLiquid().liquidAmount);
        YellowFire.transform.localScale = Vector3.one * FireSizeModifier(liquidTracker.GetSelectedLiquid().liquidAmount);

        animator.SetBool("IsOnFire", true);
    }

    public void EndFire()
    {
        isOnFire = false;
        animator.SetBool("IsOnFire", false);
        StartCoroutine(LerpFireScale(0, RedFire, true));
        StartCoroutine(LerpFireScale(0, OrangeFire, true));
        StartCoroutine(LerpFireScale(0, YellowFire, true));
    }

    private float FireSizeModifier(float size)
    {
        if (size <= 3)  return 3f;
        if (size <= 2) return 2.5f;
        if (size <= 1) return 2f;
        return 0f;
    }

    IEnumerator LerpFireScale(float size, GameObject fire, bool endFire)
    {
        float time = 0;
        float duration = secondsOnFirePerUnit;
        startValue = FireSizeModifier(size + 1);
        endValue = FireSizeModifier(size);

        while (time < duration)
        {
            scaleModifier = Mathf.Lerp(startValue, endValue, time / duration);
            fire.transform.localScale = new Vector3(scaleModifier, scaleModifier, scaleModifier);
            time += Time.deltaTime;
            yield return null;
        }
        fire.transform.localScale = new Vector3(endValue, endValue, endValue);
        scaleModifier = endValue;

        if (endFire) PoFire.SetActive(false);
    }
}
