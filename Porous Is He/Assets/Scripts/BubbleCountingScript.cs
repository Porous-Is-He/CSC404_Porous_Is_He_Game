using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static BubbleScript;

public class BubbleCountingScript : MonoBehaviour
{

    public int bubbles = 0;
    public TextMeshProUGUI bubbleText;
    // public void OnTriggerEnter(Collider other) {
    //     if (other.transform.tag == "Bubble" && other.gameObject.GetComponent<BubbleScript>().popped == false)
    //     {
    //         bubbles++;
    //         bubbleText.text = "Bubbles: " + bubbles.ToString();
    //         Debug.Log(bubbles);
    //     }
    // }

}
