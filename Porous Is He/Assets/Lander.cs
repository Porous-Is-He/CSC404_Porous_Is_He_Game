using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lander : MonoBehaviour
{
    private List<GameObject> ObjectsEntered = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool IsGrounded()
    {
        return ObjectsEntered.Count > 0;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            ObjectsEntered.Remove(other.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            ObjectsEntered.Add(other.gameObject);
        }
    }
}
