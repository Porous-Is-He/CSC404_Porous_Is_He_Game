using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggeredCheckpoint : MonoBehaviour
{
    [SerializeField] private GreasableObject greaseObj;
    [SerializeField] private Headbuttable headbuttable;

    [SerializeField] private string message;
    [SerializeField] private float time;

    private float coolDown = 6.0f;
    private float lastTriggered = -1;

    private PoMessenger poMessenger;

    void Start()
    {
        poMessenger = GameObject.Find("Player").GetComponent<PoMessenger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastTriggered < coolDown) return;

                if (headbuttable && greaseObj)
            {
                if (greaseObj.IsGreased() && !headbuttable.GetIsPushed())
                {
                    PoMessage msg = new PoMessage("It seems like maybe I can push this if I'm heavy enough.", 6);
                    poMessenger.AddReplayableMessage(msg);
                } else if (!greaseObj.IsGreased() && !headbuttable.GetIsPushed())
                {
                    PoMessage msg = new PoMessage("I might have to make the pan slippery to push this.", 6);
                    poMessenger.AddReplayableMessage(msg);
                }
            } else if (greaseObj)
            {
                if (!greaseObj.IsGreased())
                {
                    PoMessage msg = new PoMessage("This pan looks greasable.", 4);
                    poMessenger.AddReplayableMessage(msg);
                }
            } else if (headbuttable)
            {
                if (!headbuttable.GetIsPushed())
                {
                    PoMessage msg = new PoMessage("It seems like maybe I can push this if I'm heavy enough", 6);
                    poMessenger.AddReplayableMessage(msg);
                }
            } else
            {
                PoMessage msg = new PoMessage(message, time);
                poMessenger.AddReplayableMessage(msg);
            }

            lastTriggered = Time.time;
        }
    }
}