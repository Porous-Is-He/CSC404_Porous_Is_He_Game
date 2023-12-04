using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static BubblesInLevelSelect;

public class ReadBubblesScript : MonoBehaviour
{
    public BubblesInLevelSelect bubbleData;
    public TextMeshProUGUI Level1BubblesInSelect;
    public TextMeshProUGUI Level2BubblesInSelect;
    public TextMeshProUGUI Level3BubblesInSelect;

    public void Start()
    {
        bubbleData = GameObject.Find("SelectBoard").GetComponent<BubblesInLevelSelect>();
    }

    void Update()
    {
          if (bubbleData != null)
          {
            Level1BubblesInSelect.text = bubbleData.Level1Bubbles;
            Level2BubblesInSelect.text = bubbleData.Level2Bubbles;
            Level3BubblesInSelect.text = bubbleData.Level3Bubbles;
          }
          else
          {
            bubbleData = GameObject.Find("SBoard").GetComponent<BubblesInLevelSelect>();
          }
    }
}
