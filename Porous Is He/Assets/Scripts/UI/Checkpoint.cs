using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
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
        if (other.gameObject.CompareTag("Player"))
        {
            if (!triggered)
            {
                PoMessage msg = new PoMessage(message, time);
                //StartCoroutine(poMessenger.SendMessage(msg));
                poMessenger.AddReplayableMessage(msg);
                triggered = true;
            }
        }
    }
}
