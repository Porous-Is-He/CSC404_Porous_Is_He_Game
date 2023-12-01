using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCountingScript : MonoBehaviour
{

    private int coin = 0;

    public void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Bubble")
        {
            other.gameObject.GetComponent<AudioSource>().Play();
            coin++;
            Debug.Log(coin);
            Destroy(other.gameObject);
        }
    }

}
