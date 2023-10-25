using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoMessenger : MonoBehaviour
{
    public DialogueUI dialogue;
    // Start is called before the first frame update
    void Start()
    {
        PoMessage message1 = new PoMessage("I need to get out of this sink", 2);
        PoMessage message2 = new PoMessage("I have to get to the highest point", 5);
        PoMessage[] messages = { message1, message2 };
        PoSentence sentence = new PoSentence(messages);

        

        StartCoroutine(SendMessage(sentence));

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator SendMessage(PoSentence sentence)
    {
        for(int i = 0; i < sentence.messages.Length; i++)
        {
            dialogue.SetMessage(sentence.messages[i].message);
            yield return new WaitForSeconds(sentence.messages[i].time);
        }

        // reset message text field after the last message is shown
        dialogue.SetMessage("");

    }
}
