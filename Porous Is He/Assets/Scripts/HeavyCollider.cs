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


    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.I))
        {

            if (isBroken == true)
            {
                return;
            }


            if (other.gameObject.tag == "Player")
            {
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
    }
}
