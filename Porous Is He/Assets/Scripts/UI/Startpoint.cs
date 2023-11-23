using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startpoint : MonoBehaviour
{

    [SerializeField] private string[] messages = { };
    [SerializeField] private float[] time = { };
    private PoMessenger poMessenger;

    // Start is called before the first frame update
    void Start()
    {
        poMessenger = GameObject.Find("Player").GetComponent<PoMessenger>();

        PoMessage[] msgs = new PoMessage[messages.Length];
        for (int i = 0; i < messages.Length; i++)
        {
            PoMessage msg = new PoMessage(messages[i], time[i]);
            msgs[i] = msg;
        }

        //StartCoroutine(poMessenger.SendMessage(msgs));
        poMessenger.AddMessage(msgs);
    }
}
