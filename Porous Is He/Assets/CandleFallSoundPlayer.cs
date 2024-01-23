using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleFallSoundPlayer : MonoBehaviour
{

    [SerializeField] AudioSource CandleHitAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playHitSound()
    {
        CandleHitAudio.Play();
    }
}
