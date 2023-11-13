using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCheckpoint : MonoBehaviour
{

    [SerializeField] private string[] messages = { };
    [SerializeField] private float[] time = { };
    private PoMessenger poMessenger;
    private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        poMessenger = GameObject.Find("Player").GetComponent<PoMessenger>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!triggered)
            {
                PoMessage[] msgs = new PoMessage[messages.Length];
                for (int i = 0; i < messages.Length; i++)
                {
                    PoMessage msg = new PoMessage(messages[i], time[i]);
                    msgs[i] = msg;
                }

                poMessenger.AddMessage(msgs);
            }
        }
    }
}
