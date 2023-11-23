using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoMessenger : MonoBehaviour
{
    // Usage:
    // Call AddMessage() to play a subtitle
    // Call AddReplayableMessage() to play a subtitle and save it in case the player wants to replay this message

    public DialogueUI dialogue;

    private Queue<PoMessage> sentences;
    private bool displaying = false;
    private PoMessage[] lastMessages;


    void Start()
    {
        sentences = new Queue<PoMessage>();
        lastMessages = new PoMessage[] { };
    }

    void Update()
    {
        if (sentences.Count > 0 && !displaying)
        {
            displaying = true;
            StartCoroutine(DisplayMessage());
        }
    }

    public void AddMessage(PoMessage message)
    {
        sentences.Enqueue(message);
    }

    public void AddMessage(PoMessage[] messages)
    {
        foreach (PoMessage message in messages)
        {
            sentences.Enqueue(message);
        }
    }

    public void AddReplayableMessage(PoMessage message)
    {
        lastMessages = new PoMessage[] { message };
        AddMessage(message);
    }

    public void AddReplayableMessage(PoMessage[] messages)
    {
        lastMessages = messages;
        AddMessage(messages);
    }

    IEnumerator DisplayMessage()
    {
        while (sentences.Count > 0)
        {
            PoMessage poMessage = sentences.Dequeue();
            dialogue.SetMessage(poMessage.message);
/*            for (int i = 0; i < poMessage.message.Length; i++)
            {
                dialogue.SetMessage(poMessage.message.Substring(0, i));
                yield return new WaitForSeconds(0.01f);
            }*/
            yield return new WaitForSeconds(poMessage.time);
        }

        // reset message text field after the last message is shown
        dialogue.SetMessage("");
        displaying = false;
    }

    public PoMessage[] GetLastMessage() => lastMessages;



    // Update is called once per frame
    /*    void Update()
        {
            if (Input.GetKey(KeyCode.Question))
            {
                Debug.Log("clicked");
                PoMessage[] lastMessage = GetLastMessage();
                StartCoroutine(SendMessage(lastMessage));
            }
        }



        public IEnumerator SendMessage(PoMessage message)
        {
            PoMessage[] messages = { message };
            return SendMessage(messages);

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

        public PoMessage[] GetLastMessage()
        {
            PoMessage[] lastone = { messages[(int)messages.Length - 1] };
            return lastone;
        }*/
}
