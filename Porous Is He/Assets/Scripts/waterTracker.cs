using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterTracker : MonoBehaviour
{
    public static int WaterLeft = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WaterLeft >= 3)
        {
            WaterLeft = 3;
        }
    }
    private void OnGUI()
    {
        GUI.skin.box.fontSize = 10;
        GUI.Box(new Rect(100, 100, 100, 40), "Water Left:" + WaterLeft.ToString());
    }
}
