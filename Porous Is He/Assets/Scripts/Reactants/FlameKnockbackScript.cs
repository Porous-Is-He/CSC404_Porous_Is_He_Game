using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameKnockbackScript : MonoBehaviour
{
    public GameObject fireBase;
    private FlameScript flame;

    private MoverScript thisPlayer;
    private PoCombust poCombust;

    private float lastFireLevel = -1;

    private Vector3 maxScale = new Vector3(1f, 0.6f, 1f);

    private int timesTouchedFire = 0;
    private float lastTimeTouchedFire = -1;

    // Start is called before the first frame update
    void Start()
    {
        thisPlayer = GameObject.Find("Player").GetComponent<MoverScript>();
        poCombust = GameObject.Find("Player").GetComponent<PoCombust>();
        flame = fireBase.GetComponent<FlameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTimeTouchedFire > 3.0f)
        {
            timesTouchedFire = 0;
        }

        if (lastFireLevel != flame.fireLevel)
        {
            lastFireLevel = flame.fireLevel;

            transform.localScale = new Vector3(maxScale.x, maxScale.y * ( (flame.fireLevel + 7) / (flame.maxFireLevel + 7) ), maxScale.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Only want knockback to occur when the flame still exists
        // And only want knockback on the player object
        if (other.transform.gameObject.CompareTag("Player"))
        {

            if (flame.canDamage)
            {
                Vector3 moveDirection = other.transform.position - transform.position;
                moveDirection.y = 0;
                moveDirection = moveDirection.normalized;
                thisPlayer.KnockBack(moveDirection);

                lastTimeTouchedFire = Time.time;
                if (timesTouchedFire == 5)
                {
                    PoMessage msg = new PoMessage("Ow! Ow! That hurts!", 2);
                    PoMessenger poMessenger = GameObject.Find("Player").GetComponent<PoMessenger>();
                    poMessenger.AddMessage(msg);
                } else if (timesTouchedFire == 8)
                {
                    PoMessage msg = new PoMessage("Actually, that's not so bad.", 2);
                    PoMessenger poMessenger = GameObject.Find("Player").GetComponent<PoMessenger>();
                    poMessenger.AddMessage(msg);
                }
                timesTouchedFire++;

                thisPlayer.transform.GetComponent<PoSoundManager>().PlaySound("BurnDamage");


                // This handles when Po has oil and touches fire
                // it will call Combust to make Po light on fire
                if (poCombust != null) poCombust.Combust();
            }

            if (poCombust != null && PoCombust.isOnFire)
            {
                flame.isBurning = true;
            }
        }
    }
}
