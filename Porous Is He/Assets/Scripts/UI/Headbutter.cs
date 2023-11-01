using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Headbutter : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private List<GameObject> triggersEntered = new List<GameObject>();

    private float lastHeadbutt = -1;
    private float headbuttCD = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Headbutt.started += Headbutt;

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void Headbutt(InputAction.CallbackContext context)
    {
        if (Time.time - lastHeadbutt > headbuttCD)
        {
            bool hit = false;
            foreach (GameObject obj in triggersEntered)
            {
                if (obj.CompareTag("Collider"))
                {
                    if (obj.GetComponent<HeavyCollider>())
                    {
                        hit = true;
                        transform.parent.gameObject.GetComponent<PoSoundManager>().PlaySound("Headbutt_Hit");
                        obj.GetComponent<HeavyCollider>().push();
                    }

                    if (obj.GetComponent<Pushable>())
                    {
                        hit = true;
                        transform.parent.gameObject.GetComponent<PoSoundManager>().PlaySound("Headbutt_Hit");
                        obj.GetComponent<Pushable>().push();
                    }
                }
            }

            if (hit == false)
            {
                transform.parent.gameObject.GetComponent<PoSoundManager>().PlaySound("Headbutt_Miss");
            }

            lastHeadbutt = Time.time;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Collider"))
        {
            triggersEntered.Remove(other.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collider"))
        {
            triggersEntered.Add(other.gameObject);
        }
    }
}
