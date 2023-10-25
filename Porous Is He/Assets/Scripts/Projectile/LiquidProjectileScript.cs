using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidProjectileScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
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
            AudioSource.PlayClipAtPoint(audioClip, transform.position, 0.5f);
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
