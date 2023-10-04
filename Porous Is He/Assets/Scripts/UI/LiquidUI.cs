using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LiquidUI : MonoBehaviour
{

    public GameObject MeterBar1;
    public GameObject NameText;
    public GameObject AmountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Player = GameObject.Find("Player");
        LiquidTracker LiqTrack = Player.GetComponent<LiquidTracker>();
        LiquidInfo Liquid = LiqTrack.GetSelectedLiquid();

        //set text

        TextMeshProUGUI nameTM = NameText.GetComponent<TextMeshProUGUI>();
        nameTM.text = Liquid.liquidType;


        TextMeshProUGUI amountTM = AmountText.GetComponent<TextMeshProUGUI>();
        amountTM.text = Liquid.liquidAmount + " / " + LiqTrack.maxLiquidAmount;

        //set amount
        float percentFull = 1f - (float)Liquid.liquidAmount / (float)LiqTrack.maxLiquidAmount;

        //MeterBar1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
        RectTransform rt = MeterBar1.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0 + percentFull, 0);
        rt.anchorMax = new Vector2(1 + percentFull, 1);

        //set Left
        rt.offsetMin = new Vector2(0, rt.offsetMin.y);
        //set Right
        rt.offsetMax = new Vector2(0, rt.offsetMax.y);



        //swapping liquids
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int nextSelection = 0;
            if (LiqTrack.GetSelectionIndex() == 0)
                nextSelection = 1;
            else
                nextSelection = 0;

            LiqTrack.SetSelectionIndex(nextSelection);
        }
    }
}
