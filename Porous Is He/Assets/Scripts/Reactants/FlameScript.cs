using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoverScript;

public class FlameScript : MonoBehaviour, ReactantInterface
{

    //fireLevel must be between [0, maxFireLevel]
    public float fireLevel = 0;
    public float maxFireLevel = 3;

    private float lastFireLevel = -1;

    private float elapsedTime = 0.0f;
    private float animTime = 0.5f;
    
    // Variable to keep track of flames
    private bool isFlameOut = false;
    private bool isBurning = true;
    public bool isAlwaysBurning = false;

    private MoverScript thisPlayer;
    private PoCombust poCombust;

    public void ApplyLiquid(LiquidInfo liquid)
    {
        if (liquid.liquidType == "Water")
        {
            fireLevel -= 0.1f;
            if (fireLevel <= 0 && isAlwaysBurning) fireLevel = 1;
        } else if (liquid.liquidType == "Oil")
        {
            fireLevel += 0.1f;
        }

        if (fireLevel < 0) {
            fireLevel = 0;
        }

        if (fireLevel > maxFireLevel)
        {
            fireLevel = maxFireLevel;
        }

        if (fireLevel != lastFireLevel)
        {
            AudioClip audioClip = Resources.Load<AudioClip>("Sounds/Extinguish");
            AudioSource.PlayClipAtPoint(audioClip, transform.position, 0.5f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        thisPlayer = GameObject.Find("Player").GetComponent<MoverScript>();
        poCombust = GameObject.Find("Player").GetComponent<PoCombust>();
        if (fireLevel == 0)
        {
            isFlameOut = true;
            isBurning = false;
        }
            ChangeFlameSize();
    }


    // Update is called once per frame
    void Update()
    {
        if (lastFireLevel != fireLevel && isBurning)
        {
            ChangeFlameSize();
        }
    }

    private void ChangeFlameSize()
    {

        float fireMultiplier = 1.8f;
        float flameSize = transform.lossyScale.y * fireMultiplier * ((fireLevel + 2) / (maxFireLevel + 2));
        

        elapsedTime += Time.deltaTime;
        float interpolationRatio = Mathf.Max(elapsedTime / animTime, 1.0f);

        for (int i = 0; i < transform.childCount; ++i)
        {
            ParticleSystem fireParticle = transform.GetChild(i).GetComponent<ParticleSystem>();
            if (fireParticle)
            {
                var main = fireParticle.main;
                //float lastFlameSize = fireParticle.startSize;

                if (fireLevel == 0)
                {
                    fireParticle.Stop();
                    isFlameOut = true; // The flame has been put out

                }
                else
                {
                    if (transform.GetChild(i).name == "RedFire")
                    {
                        flameSize = flameSize * 0.9f;
                    }
                    if (transform.GetChild(i).name == "OrangeFire")
                    {
                        flameSize = flameSize * 0.7f;
                    }
                    else if (transform.GetChild(i).name == "YellowFire")
                    {
                        flameSize = flameSize * 0.5f;
                    }
                    //float t = lastFlameSize + (flameSize - lastFlameSize) * interpolationRatio;
                    //fireParticle.startSize = lastFlameSize + (flameSize - lastFlameSize) * interpolationRatio;
                    main.startSize = flameSize;
                    fireParticle.Play();
                    isFlameOut = false;
                }
                
            }
            

        }

        if (lastFireLevel <= 0 || fireLevel == 0)
        {
            AudioSource audioSrc = transform.GetComponent<AudioSource>();
            AudioClip audioClip = Resources.Load<AudioClip>("Sounds/FireBurning");
            audioSrc.loop = true;
            audioSrc.clip = audioClip;

            if (fireLevel == 0)
                audioSrc.Stop();
            else
                audioSrc.Play();
        }

        if (interpolationRatio >= 1)
        {
            lastFireLevel = fireLevel;
        }
    }

    public void SetIsBurning(bool isBurn)
    {
        isBurning = isBurn;
    }

    public bool GetIsBurning()
    {
        return isBurning;
    }

    private void OnTriggerEnter(Collider other)
    {   
        // Only want knockback to occur when the flame still exists
        // And only want knockback on the player object
        if (!isFlameOut && other.transform.gameObject.CompareTag("Player")) 
        {
            Vector3 moveDirection = other.transform.position - transform.position;
            moveDirection.y = 0;
            moveDirection = moveDirection.normalized;
            thisPlayer.KnockBack(moveDirection);

            // This handles when Po has oil and touches fire
            // it will call Combust to make Po light on fire
            if (poCombust != null) poCombust.Combust();
        }
    }
}
