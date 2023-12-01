using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleCountingScript : MonoBehaviour
{

    private int bubbles = 0;
    public TextMeshProUGUI bubbleText;

    public void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Bubble")
        {
            other.gameObject.GetComponent<AudioSource>().Play();
            bubbles++;
            bubbleText.text = "Bubbles: " + bubbles.ToString();
            Debug.Log(bubbles);
            Destroy(other.gameObject);
        }
    }

}
