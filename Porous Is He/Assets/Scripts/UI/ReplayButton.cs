using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L)) { 
            PoMessage[] lastMessage = GameObject.Find("Player").GetComponent<PoMessenger>().GetLastMessage();
            StartCoroutine(GameObject.Find("Player").GetComponent<PoMessenger>().SendMessage(lastMessage));
        }
    }
}