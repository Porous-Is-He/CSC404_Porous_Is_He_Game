using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyCollider : MonoBehaviour
{

    public GameObject brokenObject;
    public GameObject currentObject;
    public bool isBroken = false;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


    public void breakObject()
    {

        if (isBroken == true)
        {
            return;
        }

        int playerWeight = GameObject.Find("Player").GetComponent<LiquidTracker>().CalcWeight();

        if (playerWeight >= GameObject.Find("Player").GetComponent<LiquidTracker>().maxLiquidAmount)
        {
            //GameObject broken = Instantiate(brokenObject, currentObject.transform.position, currentObject.transform.rotation, currentObject.transform.parent);
            //broken.transform.localScale = currentObject.transform.localScale;
            brokenObject.SetActive(true);
            currentObject.SetActive(false);
            isBroken = true;
        }

    }

}
