using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveSpongeBobWater : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {

            //WaterTracker.SetWaterAmount(WaterTracker.maxWaterAmount);
            GameObject.Find("waterUI").GetComponent<WaterTracker>().SetWaterAmount(3);
        }

        
    }

    
}