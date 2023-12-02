using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static BubbleScript;

public class BubbleCountingScript : MonoBehaviour
{

    private int bubbles = 0;
    private BubbleScript currentBubble;
    public TextMeshProUGUI bubbleText;

    public void Start()
    {
    currentBubble = FindObjectOfType<BubbleScript>();
    }

    public void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Bubble")
        {
            bubbles++;
            bubbleText.text = "Bubbles: " + bubbles.ToString();
            Debug.Log(bubbles);
        }
    }

}
