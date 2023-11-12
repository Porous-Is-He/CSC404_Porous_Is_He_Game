using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is in charge of the "Bubbles"
// The main thing is that it has a floating animation
// As well as disappearing when collided by the player
public class BubbleScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float amplitude = 0.1f;
    public float frequency = 1f;
    private Vector3 posOffset = new Vector3 ();
    private Vector3 tempPosition = new Vector3 ();

    public void Start()
    {
        GetComponent<Renderer>().enabled = true;
        tempPosition = transform.position;
    }

    public void Update()
    {

        // Float up/down with a Sin()
        tempPosition = posOffset;
        tempPosition.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPosition;
    }

    public void OnTriggerEnter(Collider other) {
        if (other.transform.gameObject.CompareTag("Player"))
        {

            GetComponent<Renderer>().enabled = false;
        }

    }
}
