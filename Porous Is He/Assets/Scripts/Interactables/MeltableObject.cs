using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeltableObject : MonoBehaviour
{
    private bool melted = false;
    private Animator animator;

    private PoMessenger poMessenger;
    private float coolDown = 20.0f;
    private float lastTriggered = -1;

    void Start()
    {
        animator = GetComponent<Animator>();
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
            if (PoCombust.isOnFire && !melted)
            {
                if (animator != null)
                {
                    melted = true;
                    animator.SetBool("isMelting", melted);

                    gameObject.GetComponent<AudioSource>().Play();
                }
            } else if (!melted && Time.time - lastTriggered > coolDown)
            {
                PoMessage msg = new PoMessage("I'll need to be on fire to melt these ice cubes.", 6);
                poMessenger.AddReplayableMessage(msg);
                lastTriggered = Time.time;
            }
        }
    }

    public bool IsMelted() => melted;
}
