using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BalanceScale : MonoBehaviour
{

    // when bar is rotated at x = 0 initially,
    // scaleObject1 (lighter) is the scale that is NOT on the floor.

    [SerializeField] GameObject scaleObject1;
    [SerializeField] GameObject scaleObject2;
    [SerializeField] GameObject Bar;

    // Bar angle variables
    private float straightAngle = 25;
    private float minAngle = 0;
    private float maxAngle = 50;

    // Scale weights 
    private Scale scale1;
    private Scale scale2;

    // Y position value for scale
    private float maxValueY = -4.9f;
    private float minValueY = -14.13f;
    private float middleValueY = -9.515f;

    // Z position value for scale
    private float maxValueZ = 11f;
    private float minValueZ = 10f;

    bool changed;


    // Start is called before the first frame update
    void Start()
    {
        scale1 = scaleObject1.GetComponent<Scale>();
        scale2 = scaleObject2.GetComponent<Scale>();
        // set initial bar rotation
        Bar.transform.localEulerAngles = new Vector3(0, 180, 0);
        changed = true;
    }

    // Update is called once per frame
    void Update()
    {
        float weight1 = scale1.GetWeight();
        float weight2 = scale2.GetWeight();
        float difference = weight1 - weight2;
        Vector3 barRotation = Bar.transform.localEulerAngles;

        RotateBar(barRotation, straightAngle + difference);
        UpdatePositionY();
        UpdatePositionZ();
        changed = false;
    }

    private void RotateBar(Vector3 oldRotation, float newX)
    {
        newX = (newX < minAngle) ? minAngle : newX;
        newX = (newX > maxAngle) ? maxAngle : newX;

        if (oldRotation.x > newX)
        {
            oldRotation.x -= 0.1f;
            if (oldRotation.x <= newX) oldRotation.x = newX;
            Bar.transform.localEulerAngles = oldRotation;
            changed = true;
        } else if (oldRotation.x < newX)
        {
            oldRotation.x += 0.1f;
            if (oldRotation.x >= newX) oldRotation.x = newX;
            Bar.transform.localEulerAngles = oldRotation;
            changed = true;
        }
    }
    private void UpdatePositionY()
    {
        if (!changed) return;
        float xRotation = Bar.transform.localEulerAngles.x;
        Vector3 pos1 = scaleObject1.transform.localPosition;
        Vector3 pos2 = scaleObject2.transform.localPosition;
        
        float newPos2Y = Utils.ConvertRatio(minAngle, maxAngle, xRotation, minValueY, maxValueY);
        scaleObject2.transform.localPosition = new Vector3(pos2.x, newPos2Y, pos2.z);

        float newPos1Y = middleValueY - (newPos2Y - middleValueY);
        scaleObject1.transform.localPosition = new Vector3(pos1.x, newPos1Y, pos1.z);
    }

    private void UpdatePositionZ()
    {
        if (!changed) return;
        float xRotation = Bar.transform.localEulerAngles.x;
        Vector3 pos1 = scaleObject1.transform.localPosition;
        Vector3 pos2 = scaleObject2.transform.localPosition;
        float newPosZ;

        if (xRotation < straightAngle) 
            newPosZ = Utils.ConvertRatio(minAngle, straightAngle, xRotation, minValueZ, maxValueZ);
        else if (xRotation > straightAngle)
            newPosZ = Utils.ConvertRatio(maxAngle, straightAngle, xRotation, minValueZ, maxValueZ);
        else
            newPosZ = maxValueZ;

        scaleObject1.transform.localPosition = new Vector3(pos1.x, pos1.y, -newPosZ);
        scaleObject2.transform.localPosition = new Vector3(pos2.x, pos2.y, newPosZ);
    }

}
