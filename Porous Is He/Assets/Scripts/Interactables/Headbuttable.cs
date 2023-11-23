using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbuttable : MonoBehaviour
{
    private bool isPushed = false;
    private GameObject Player;
    private Animator animator;
    [SerializeField] private bool canBePushedAgain = false;
    [SerializeField] private GreasableObject greaseObj;
    [SerializeField] private FillableCup fillableCupObj;
    [SerializeField] private AudioSource crashSoundSrc = null;

    private bool allow = true;

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

    public void playHitSound()
    {
        if (crashSoundSrc != null)
        {
            crashSoundSrc.Play();
        }
    }

    public void push()
    {
        if (!allow) return;
        if (isPushed && !canBePushedAgain) return;
        if (isPushed && canBePushedAgain)
        {
            PushReturn(); return;
        }
            
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

    private void PushReturn()
    {
        if (Player.GetComponent<LiquidTracker>().IsHeavy())
        {
            if (animator != null)
            {
                animator.SetBool("isPushed", false);
                isPushed = false;
            }
        }

    }

    public bool GetIsPushed() => isPushed;
    public void SetAllow(bool isAllowed)
    {
        allow = isAllowed;
    }

}
