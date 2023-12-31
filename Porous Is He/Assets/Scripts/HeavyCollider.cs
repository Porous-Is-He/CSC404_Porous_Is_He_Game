using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyCollider : MonoBehaviour
{

    public GameObject brokenObject;
    public GameObject currentObject;
    public bool isBroken = false;

    private bool hintGiven = false;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

    }


     void OnTriggerEnter(Collider other)
    {

        if (!hintGiven && other.gameObject.name.StartsWith("WaterProjectile"))
        {
            Invoke("GiveHint", 2);
            hintGiven = true;
        }
    }

    public void push()
    {
        if (isBroken == true) return;

        if (GameObject.Find("Player").GetComponent<LiquidTracker>().IsHeavy())
        {
            //GameObject broken = Instantiate(brokenObject, currentObject.transform.position, currentObject.transform.rotation, currentObject.transform.parent);
            //broken.transform.localScale = currentObject.transform.localScale;
            brokenObject.SetActive(true);
            currentObject.SetActive(false);
            isBroken = true;

            if (gameObject.GetComponent<AudioSource>())
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }

    private void GiveHint()
    {
        PoMessenger poMessenger = GameObject.Find("Player").GetComponent<PoMessenger>();
        PoMessage[] msg = { 
            new PoMessage("Hmmm, shooting water isn't going to break it.", 4),
            new PoMessage("Maybe if I absorb enough water, I'll be heavy enough to break through it.", 6)};
        //StartCoroutine(messenger.SendMessage(msg));
        poMessenger.AddReplayableMessage(msg);
    }
}
