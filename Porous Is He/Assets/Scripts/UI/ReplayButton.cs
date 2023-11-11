using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReplayButton : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.RepeatMsg.started += RepeatMsg;

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.L)) { 
        //    PoMessage[] lastMessage = GameObject.Find("Player").GetComponent<PoMessenger>().GetLastMessage();
        //    StartCoroutine(GameObject.Find("Player").GetComponent<PoMessenger>().SendMessage(lastMessage));
        //}
    }


    private void RepeatMsg(InputAction.CallbackContext context)
    {
        PoMessage[] lastMessage = GameObject.Find("Player").GetComponent<PoMessenger>().GetLastMessage();
        //StartCoroutine(GameObject.Find("Player").GetComponent<PoMessenger>().SendMessage(lastMessage));
        GameObject.Find("Player").GetComponent<PoMessenger>().AddMessage(lastMessage);
    }
}