using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoverScript;

public class FlameScript : MonoBehaviour, ReactantInterface
{

    //fireLevel must be between [0, maxFireLevel]
    public int fireLevel = 0;
    public int maxFireLevel = 3;

    private int lastFireLevel = -1;

    private float elapsedTime = 0.0f;
    private float animTime = 0.5f;

    private MoverScript thisPlayer;

    public void ApplyLiquid(LiquidInfo liquid)
    {
        Debug.Log("Hello");

        if (liquid.liquidType == "Water")
        {
            fireLevel--;
        } else if (liquid.liquidType == "Oil")
        {
            fireLevel++;
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
        thisPlayer = FindObjectOfType<MoverScript>();
    }


    // Update is called once per frame
    void Update()
    {
        if (lastFireLevel != fireLevel)
        {
            //var fireSprite = this.transform.parent.Find("FireSprite");
            //var newScale = new Vector3(1.2f, 1.2f * fireLevel, 1.2f);
            //fireSprite.transform.localScale = newScale;
            //fireSprite.transform.localPosition = new Vector3(0f, newScale.y * 0.5f, 0f);

            //var fireSprite = gameObject;
            //var newScale = new Vector3(1.0f, 1.0f * (float)fireLevel / 3.0f + 0.1f, 1.0f);
            //fireSprite.transform.localScale = newScale;
            //fireSprite.transform.localPosition = new Vector3(0f, newScale.y * 0.5f, 0f);



            float fireMultiplier = 1.8f;
            float flameSize = transform.lossyScale.y * fireMultiplier * ( ((float)fireLevel + 2) / ((float)maxFireLevel + 2) );

            elapsedTime += Time.deltaTime;
            float interpolationRatio = Mathf.Max(elapsedTime / animTime, 1.0f);

            for (int i = 0; i < transform.childCount; ++i)
            {
                ParticleSystem fireParticle = transform.GetChild(i).GetComponent<ParticleSystem>();
                if (fireParticle)
                {
                    float lastFlameSize = fireParticle.startSize;

                    if (fireLevel == 0)
                    {
                        fireParticle.Stop();
                        
                    }
                    else { 
                        if (transform.GetChild(i).name == "OrangeFire")
                        {
                            flameSize = flameSize * 0.7f;
                        } else if (transform.GetChild(i).name == "YellowFire")
                        {
                            flameSize = flameSize * 0.5f;
                        }
                        fireParticle.startSize = (float)lastFlameSize + (float)(flameSize - lastFlameSize) * interpolationRatio;
                        fireParticle.Play();
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

            if (interpolationRatio >= 1) {
                lastFireLevel = fireLevel;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Knockback the sponge");
        Vector3 moveDirection = other.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
        thisPlayer.KnockBack(moveDirection);
    }
}
