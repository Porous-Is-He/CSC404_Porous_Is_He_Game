using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialogueTextField;

    private string currentMessage;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI textfield = dialogueTextField.GetComponent<TextMeshProUGUI>();
        textfield.text = currentMessage;
    }

    public void SetMessage(string message)
    {
        currentMessage = message;
    }

    public string GetMessage()
    {
        return currentMessage;
    }
}
