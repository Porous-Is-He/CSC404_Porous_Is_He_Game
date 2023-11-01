using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCheckpoint : MonoBehaviour
{

    [SerializeField] private string message = "";
    [SerializeField] private float time = 2;
    private PoMessenger poMessenger;
    private bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        poMessenger = GameObject.Find("Player").GetComponent<PoMessenger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {

            if (other.gameObject.GetComponent<LiquidTracker>().CalcWeight() > 0 && !triggered)
            {
                PoMessage msg = new PoMessage(message, time);
                PoMessage[] messages = { msg };
                StartCoroutine(poMessenger.SendMessage(messages));
                triggered = true;
            }
        }
    }
}
