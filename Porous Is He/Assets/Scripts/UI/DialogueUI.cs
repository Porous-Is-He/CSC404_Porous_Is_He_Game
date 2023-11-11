using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialogueTextField;

    [SerializeField] private Image textBg;
    [SerializeField] private Image dot1;
    [SerializeField] private Image dot2;
    [SerializeField] private Image dot3;

    private string currentMessage;
    TextMeshProUGUI textfield;
    private bool hidden;

    // Start is called before the first frame update
    void Start()
    {
        textfield = dialogueTextField.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMessage == "" || currentMessage == null)
            HideBackground();
        else
        {
            if (hidden) StartCoroutine(ShowBackground());
        }
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

    private void HideBackground()
    {
        textBg.enabled = false;
        dot1.enabled = false;
        dot2.enabled = false;
        dot3.enabled = false;
        hidden = true;
    }

    IEnumerator ShowBackground()
    {
        hidden = false;
        textBg.enabled = true;
        yield return new WaitForSeconds(0.1f);
        dot1.enabled = true;
        yield return new WaitForSeconds(0.1f);
        dot2.enabled = true;
        yield return new WaitForSeconds(0.1f);
        dot3.enabled = true;
    }
}
