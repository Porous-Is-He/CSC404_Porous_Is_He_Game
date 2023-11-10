using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BalanceScale : MonoBehaviour
{

    // when bar is rotated at z = 115 initially,
    // scaleObject1 (heavier) is the scale that is on the floor.

    [SerializeField] GameObject scaleObject1;
    [SerializeField] GameObject scaleObject2;
    [SerializeField] GameObject Bar;

    // Bar angle variables
    private float straightAngle = 90;
    private float minAngle = 65;
    private float maxAngle = 115;

    // Scale weights 
    private Scale scale1;
    private Scale scale2;

    // Y position value for scale
    private float maxValueY = 0;
    private float minValueY = -14.5f;
    private float middleValueY = -7.25f;


    // Start is called before the first frame update
    void Start()
    {
        scale1 = scaleObject1.GetComponent<Scale>();
        scale2 = scaleObject2.GetComponent<Scale>();
    }

    // Update is called once per frame
    void Update()
    {
        float weight1 = scale1.GetWeight();
        float weight2 = scale2.GetWeight();
        float difference = weight1 - weight2;
        Vector3 barRotation = Bar.transform.localEulerAngles;

        RotateBar(barRotation, straightAngle + difference);
        UpdateScalePositionY();
    }

    private void RotateBar(Vector3 oldRotation, float newZ)
    {
        newZ = (newZ < minAngle) ? minAngle : newZ;
        newZ = (newZ > maxAngle) ? maxAngle : newZ;

        if (oldRotation.z > newZ)
        {
            oldRotation.z -= 0.1f;
            if (oldRotation.z <= newZ) oldRotation.z = newZ;
            Bar.transform.localEulerAngles = oldRotation;
        } else if (oldRotation.z < newZ)
        {
            oldRotation.z += 0.1f;
            if (oldRotation.z >= newZ) oldRotation.z = newZ;
            Bar.transform.localEulerAngles = oldRotation;
        }
    }
    private void UpdateScalePositionY()
    {
        float zRotation = Bar.transform.localEulerAngles.z;
        Vector3 pos1 = scaleObject1.transform.localPosition;
        Vector3 pos2 = scaleObject2.transform.localPosition;
        
        float newPos2Y = Utils.ConvertRatio(minAngle, maxAngle, zRotation, minValueY, maxValueY);
        scaleObject2.transform.localPosition = new Vector3(pos2.x, newPos2Y, pos2.z);

        float newPos1Y = middleValueY - (newPos2Y - middleValueY);
        scaleObject1.transform.localPosition = new Vector3(pos1.x, newPos1Y, pos1.z);
    }

}
