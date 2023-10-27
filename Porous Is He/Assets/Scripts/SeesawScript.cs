using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawScript : MonoBehaviour
{

    public GameObject PositiveZone;
    public GameObject NegativeZone;
    public GameObject Platform;

    private float rotSpdFactor = 0.2f;
    private float maxRotation = 30.0f;

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

        if (other.gameObject.tag == "Player")
        {
            int playerWeight = GameObject.Find("Player").GetComponent<LiquidTracker>().CalcWeight();

            if (playerWeight >= GameObject.Find("Player").GetComponent<LiquidTracker>().maxLiquidAmount)
            {

                float maxDist = (NegativeZone.transform.position - PositiveZone.transform.position).magnitude / 2;
                float negativeDist = (NegativeZone.transform.position - other.transform.position).magnitude;
                float positiveDist = (PositiveZone.transform.position - other.transform.position).magnitude;

                float rotationSpdRatio = 0.0f;

                if (negativeDist < positiveDist)
                {
                    rotationSpdRatio = -maxDist / negativeDist;
                }
                else
                {
                    rotationSpdRatio = maxDist / positiveDist;
                }
                float rotationSpd = rotSpdFactor * rotationSpdRatio;

                float rot = Platform.transform.localRotation.eulerAngles.x + rotationSpd;
                if ( rot > 180 ){
                    rot -= 360;
                }

                if (rot > maxRotation)
                {
                    Quaternion newRotation = Quaternion.Euler(maxRotation, Platform.transform.localRotation.eulerAngles.y, Platform.transform.localRotation.eulerAngles.z);
                    Platform.transform.localRotation = newRotation;
                }
                else if (rot < -maxRotation)
                {
                    Quaternion newRotation = Quaternion.Euler(-maxRotation, Platform.transform.localRotation.eulerAngles.y, Platform.transform.localRotation.eulerAngles.z);
                    Platform.transform.localRotation = newRotation;
                }
                else
                {
                    Platform.transform.Rotate(rotationSpd, 0, 0, Space.Self);
                }
            }
        }
    }
}
