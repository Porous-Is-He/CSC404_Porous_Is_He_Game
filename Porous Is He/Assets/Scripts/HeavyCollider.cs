using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyCollider : MonoBehaviour
{

    public GameObject brokenObject;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

    }


     void OnCollisionEnter(Collision collision)
    {
        int playerWeight = GameObject.Find("Player").GetComponent<LiquidTracker>().CalcWeight();

        if (collision.gameObject.tag == "Player"
           && playerWeight >= GameObject.Find("Player").GetComponent<LiquidTracker>().maxLiquidAmount)
        {
            Instantiate(brokenObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
