using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    private bool isPushed = false;
    private GameObject Player;
    private Animator animator;
    [SerializeField] private bool canBePushedAgain = false;
    [SerializeField] private GreasableObject greaseObj;
    [SerializeField] private FillableCup fillableCupObj;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    public void push()
    {
        if (isPushed && !canBePushedAgain) return;
        if (greaseObj)
        {
            if (!greaseObj.IsGreased()) return;
        }

        if (Player.GetComponent<LiquidTracker>().IsHeavy())
        {
            if (animator != null)
            {
                if (fillableCupObj)
                {
                    fillableCupObj.EmptyCup();
                    animator.SetTrigger("push");
                    return;
                }
                animator.SetBool("isPushed", true);
                isPushed = true;
            }
        }
    }
}
