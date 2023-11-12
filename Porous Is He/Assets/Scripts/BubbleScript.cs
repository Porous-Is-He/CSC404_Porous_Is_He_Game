using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    // Start is called before the first frame update

    public void Start()
    {
        GetComponent<Renderer>().enabled = true;
    }

    public void OnTriggernEnter(Collider other) {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision Detected.");
            GetComponent<Renderer>().enabled = false;
        }

        Debug.Log("Bubble should be invisible here.");
    }
}
