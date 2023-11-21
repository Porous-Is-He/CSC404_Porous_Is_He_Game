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
    private bool wasBurning = false;
    public bool isBurning = false;
    public bool canDamage = false;
    public bool isAlwaysBurning = false;
    public bool startOff = false;

    public GameObject SmokeEmitter;
    public GameObject OilSmokeEmitter;

    private Transform parent;

    public void ApplyLiquid(LiquidInfo liquid)
    {
        if (liquid.liquidType == "Water")
        {
            fireLevel -= liquid.liquidAmount * 2;
            //if (fireLevel <= 0 && isAlwaysBurning) fireLevel = 1;

            if (fireLevel > 0 && isBurning)
            {
                SmokeEmitter.GetComponent<ParticleSystem>().Emit(5);
            }
        } else if (liquid.liquidType == "Oil")
        {
            if (fireLevel < maxFireLevel && isBurning)
            {
                OilSmokeEmitter.GetComponent<ParticleSystem>().Emit(5);
            }
            fireLevel += liquid.liquidAmount * 2;
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
        wasBurning = isBurning;
        parent = transform.parent.transform;

        float oldFireLevel = fireLevel;
        if (startOff)
        {
            fireLevel = 0;
        }

        ChangeFlameSize();

        if (startOff)
        {
            fireLevel = oldFireLevel;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (lastFireLevel != fireLevel && isBurning)
        {
            ChangeFlameSize();
        }

        if (wasBurning != isBurning)
        {
            ChangeFlameSize();
        }
        wasBurning = isBurning;
    }

    private float minSpd = 2;
    private float maxSpd = 6;
    private float minLife = 5;
    private float maxLife = 7;

    private void ChangeFlameSize()
    {
        if (fireLevel == 0)
        {
            canDamage = false;
            if (isAlwaysBurning == false) 
            {
                isBurning = false;
            }
        }

        float fireMultiplier = 1.7f;
        float flameSize = transform.lossyScale.x * fireMultiplier * ((fireLevel + 6) / (maxFireLevel + 6));

        float fireSpeed = transform.lossyScale.x * 0.3f * ((fireLevel / maxFireLevel) * (maxSpd - minSpd) + minSpd);
        float fireLife = maxLife - ( (fireLevel / maxFireLevel) * (maxLife - minLife) );


        if (fireLevel == 0)
        {
            flameSize = transform.lossyScale.x * fireMultiplier * 0.15f;
            fireSpeed = fireSpeed / 4;
        }

        elapsedTime += Time.deltaTime;
        float interpolationRatio = Mathf.Max(elapsedTime / animTime, 1.0f);

        for (int i = 0; i < parent.childCount; ++i)
        {
            if (parent.GetChild(i).name == "Smoke" || parent.GetChild(i).name == "OilSmoke")
            {
                continue;
            }

                ParticleSystem fireParticle = parent.GetChild(i).GetComponent<ParticleSystem>();
            if (fireParticle)
            {
                var main = fireParticle.main;
                //float lastFlameSize = fireParticle.startSize;

                if (fireLevel == 0 && isAlwaysBurning == false)
                {
                    fireParticle.Stop();

                }
                else
                {

                    if (parent.GetChild(i).name == "RedFire")
                    {
                        flameSize = flameSize * 0.9f;
                    }
                    if (parent.GetChild(i).name == "OrangeFire")
                    {
                        flameSize = flameSize * 0.7f;
                    }
                    else if (parent.GetChild(i).name == "YellowFire")
                    {
                        flameSize = flameSize * 0.5f;
                    }
                    //float t = lastFlameSize + (flameSize - lastFlameSize) * interpolationRatio;
                    //fireParticle.startSize = lastFlameSize + (flameSize - lastFlameSize) * interpolationRatio;
                    main.startSize = flameSize;
                    ParticleSystem.VelocityOverLifetimeModule velocityModule = fireParticle.velocityOverLifetime;
                    velocityModule.yMultiplier = fireSpeed;
                    fireParticle.Play();

                    isBurning = true;
                    if (fireLevel > 0)
                    {
                        canDamage = true;
                    }
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

}
