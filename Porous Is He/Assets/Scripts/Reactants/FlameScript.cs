using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour, ReactantInterface
{

    //fireLevel must be between [0, maxFireLevel]
    public int fireLevel = 0;
    public int maxFireLevel = 3;

    private int lastFireLevel = -1;

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

            if (lastFireLevel <= 0 || fireLevel == 0)
            {
                Transform[] fireEffects = gameObject.GetComponentsInChildren<Transform>(transform);

                for (int i = 0; i < transform.childCount; ++i)
                {
                    ParticleSystem fireParticle = transform.GetChild(i).GetComponent<ParticleSystem>();
                    if (fireParticle)
                    {
                        if (fireLevel == 0)
                            fireParticle.Stop();
                        else
                            fireParticle.Play();
                    }

                }

                AudioSource audioSrc = transform.GetComponent<AudioSource>();
                AudioClip audioClip = Resources.Load<AudioClip>("Sounds/FireBurning");
                audioSrc.loop = true;
                audioSrc.clip = audioClip;

                if (fireLevel == 0)
                    audioSrc.Stop();
                else
                    audioSrc.Play();
            }

            lastFireLevel = fireLevel;
        }
    }
}
