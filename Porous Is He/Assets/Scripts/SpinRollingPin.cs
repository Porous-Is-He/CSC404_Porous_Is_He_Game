using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinRollingPin : MonoBehaviour
{
    public float rotationSpeed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, rotationSpeed, 0, Space.Self);
    }
}
