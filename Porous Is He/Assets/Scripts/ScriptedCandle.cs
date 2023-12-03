using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedCandle : MonoBehaviour
{

    [SerializeField] Animator CandleAnimator;

    public bool triggered = false;
    
    [SerializeField] AudioSource CandleAudio;

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
            CandleAudio.Play();
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

}
