using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoSoundManager : MonoBehaviour
{

    private int headbuttMissCounter = 2;
    private int headbuttCounter = 1;

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
        string soundDir = "Sounds/SFX/Character Sounds/";

        if (soundType == "Shoot")
        { 
            soundDir += "sfx_shoot_nl_01";
        }
        else if (soundType == "Absorb")
        {
            soundDir += "sfx_slurp_nl_01";
        }
        else if (soundType == "Headbutt_Miss")
        {
            soundDir += "sfx_HdbtMiss_nl_" + headbuttMissCounter.ToString();
            headbuttMissCounter += 2;
            if (headbuttMissCounter > 4)
            {
                headbuttMissCounter = 2;
            }
        }
        else if (soundType == "Jump")
        {
            soundDir += "sfx_jump_nl_02";
        }
        else if (soundType == "DoubleJump")
        {
            soundDir += "sfx_jump_nl_01";
        }
        else if (soundType == "Headbutt_Hit")
        {
            soundDir += "sfx_HdbtHit_nl_" + headbuttCounter.ToString();
            headbuttCounter++;
            if (headbuttCounter > 6)
            {
                headbuttCounter = 1;
            }
        }
        else if (soundType == "BurnDamage")
        {
            soundDir += "sfx_burndamage_nl_01";
        }
        else if (soundType == "Release")
        {
            soundDir += "sfx_squeeze_nl_01";
        }
        else if (soundType == "NoLiquid")
        {
            soundDir += "sfx_noliquid_nl_01";
        }
        else
        {
            Debug.Log("No such sound: " + soundType);
        }


        AudioSource audioSrc = transform.Find("Audio Source").GetComponent<AudioSource>();
        AudioClip audioClip = Resources.Load<AudioClip>(soundDir);
        audioSrc.PlayOneShot(audioClip);
    }
}