using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoSoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string soundType)
    {
        string soundDir = "Sounds/";

        if (soundType == "Shoot")
            soundDir += "PoShoot";
        else if (soundType == "Absorb")
            soundDir += "PoSlurp";
        else
            Debug.Log("No such sound: " + soundType);


        AudioSource audioSrc = transform.Find("Audio Source").GetComponent<AudioSource>();
        AudioClip audioClip = Resources.Load<AudioClip>(soundDir);
        audioSrc.PlayOneShot(audioClip);
    }
}
