using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoMessenger : MonoBehaviour
{
    public DialogueUI dialogue;
    // Start is called before the first frame update
    void Start()
    {
        PoMessage message = new PoMessage("I'm trapped under a bowl. I need to break my way out.", 5);
        PoMessage[] messages = { message };

        StartCoroutine(SendMessage(messages));

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator SendMessage(PoMessage[] messages)
    {
        for(int i = 0; i < messages.Length; i++)
        {
            dialogue.SetMessage(messages[i].message);
            yield return new WaitForSeconds(messages[i].time);
        }

        // reset message text field after the last message is shown
        dialogue.SetMessage("");

    }
}
