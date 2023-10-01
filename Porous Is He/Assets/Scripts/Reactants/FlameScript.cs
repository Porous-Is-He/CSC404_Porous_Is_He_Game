using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour, ReactantInterface
{

    //fireLevel must be between [0, maxFireLevel]
    public int fireLevel = 0;
    public int maxFireLevel = 3;

    private int lastFireLevel = -1;

    public void ApplyLiquid(LiquidInfo liquid)
    {
        if (liquid.liquidType == "Water")
        {
            fireLevel--;
        } else if (liquid.liquidType == "Oil")
        {
            fireLevel++;
        }


        if (fireLevel < 0) {
            fireLevel = 0;
        }

        if (fireLevel > maxFireLevel)
        {
            fireLevel = maxFireLevel;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lastFireLevel != fireLevel)
        {
            var fireSprite = this.transform.parent.Find("FireSprite");
            var newScale = new Vector3(1.2f, 1.2f * fireLevel, 1.2f);
            fireSprite.transform.localScale = newScale;
            fireSprite.transform.position = new Vector3(0f, newScale.y * 0.5f, 0f);
        }
    }
}
