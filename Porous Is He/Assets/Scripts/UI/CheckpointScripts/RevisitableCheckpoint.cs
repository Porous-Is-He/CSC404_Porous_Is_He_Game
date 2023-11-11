using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevisitableCheckpoint : MonoBehaviour
{

    [SerializeField] private string[] messages = { };
    [SerializeField] private float[] time = { };
    private PoMessenger poMessenger;
    private int timesTriggered;
    private float coolDown = 10.0f;
    private float lastTriggered = -1;
    // Start is called before the first frame update
    void Start()
    {
        timesTriggered = 0;
        poMessenger = GameObject.Find("Player").GetComponent<PoMessenger>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (Time.time - lastTriggered > coolDown)
        {
            PoMessage msg = new PoMessage(messages[timesTriggered], time[timesTriggered]);
            //StartCoroutine(poMessenger.SendMessage(msg));
            poMessenger.AddMessage(msg);

            timesTriggered++;
            if (timesTriggered >= messages.Length)
            {
                timesTriggered = messages.Length - 1;
            }
        }
        lastTriggered = Time.time;
    }
}
