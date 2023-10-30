using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LiquidProjectileScript : MonoBehaviour
{

    [SerializeField] private GameObject liquidSplashEffect;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 6);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public LiquidInfo liquid;

    public void SetLiquid(LiquidInfo liquid)
    {
        this.liquid = liquid;
    }

    private void actOnHitObject(GameObject hitObject)
    {
        if (hitObject.CompareTag("Reactant"))
        {
            ReactantInterface reactant = hitObject.GetComponent<ReactantInterface>();

            reactant.ApplyLiquid(this.liquid);
            // Put out fire
            //Destroy(other.gameObject);

        }
        else
        {
            AudioClip audioClip = Resources.Load<AudioClip>("Sounds/WaterHitFloor");
            AudioSource.PlayClipAtPoint(audioClip, transform.position, 0.4f);
            
            // create splash effect
            GameObject splash = Instantiate(liquidSplashEffect, transform.position, Quaternion.identity);
            Destroy(splash, 2);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        actOnHitObject(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        actOnHitObject(other.gameObject);
    }
}
