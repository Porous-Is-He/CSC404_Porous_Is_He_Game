using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltableObject : MonoBehaviour
{
    private bool melted = false;
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
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
                }
            }
        }
    }
}
