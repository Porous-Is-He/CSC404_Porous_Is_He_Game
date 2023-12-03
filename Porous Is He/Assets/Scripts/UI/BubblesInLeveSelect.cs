using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubblesInLevelSelect : MonoBehaviour
{

    public static BubblesInLevelSelect control;
    public bool sceneIsSwitched = false;

    public TextMeshProUGUI Level1Bubbles;
    public TextMeshProUGUI Level2Bubbles;
    public TextMeshProUGUI Level3Bubbles;

    public void Start()
    {
        if (control != null)
        {
            Destroy(this.gameObject);
            return;
        }

        control = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public void Update()
    {
        if (sceneIsSwitched == false)
        {
            this.gameObject.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<Canvas>().enabled = false;
        }
    }
}
