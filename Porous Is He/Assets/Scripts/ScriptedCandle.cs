using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedCandle : MonoBehaviour
{

    [SerializeField] Animator CandleAnimator;

    public bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player") && !triggered)
        {
            CandleAnimator.SetBool("isPushed", true);
            triggered = true;

            MoverScript MS = GameObject.Find("Player").GetComponent<MoverScript>();
            MS.cannotMove = true;

            StartCoroutine(EnablePoMovement());
        }
    }

    private IEnumerator EnablePoMovement()
    {
        yield return new WaitForSeconds(1);

        MoverScript MS = GameObject.Find("Player").GetComponent<MoverScript>();
        MS.cannotMove = false;
    }

    public void playHitSound()
    {
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

}
