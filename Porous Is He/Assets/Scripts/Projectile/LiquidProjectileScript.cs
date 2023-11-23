using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class LiquidProjectileScript : MonoBehaviour
{

    [SerializeField] private GameObject liquidSplashEffect;

    private bool isActive = true;

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

    private void destroyProjectile(bool hitSurface)
    {
        if (hitSurface == true)
        {
            AudioClip audioClip = Resources.Load<AudioClip>("Sounds/WaterHitFloor");
            AudioSource.PlayClipAtPoint(audioClip, transform.position, 0.4f);

            // create splash effect
            GameObject splash = Instantiate(liquidSplashEffect, transform.position, Quaternion.identity);
            Destroy(splash, 2);
        }

        isActive = false;
        transform.Find("Particle System").GetComponent<ParticleSystem>().Stop();
        transform.gameObject.GetComponent<Collider>().enabled = false;
        transform.gameObject.GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, 2);
    }

    private void actOnHitObject(GameObject hitObject, bool doDestroy)
    {
        if (!isActive)
        {
            return;
        }

        if (hitObject.CompareTag("Reactant"))
        {
            ReactantInterface reactant = hitObject.GetComponent<ReactantInterface>();

            reactant.ApplyLiquid(this.liquid);
            // Put out fire
            //Destroy(other.gameObject);

        } 
        else
        {
            if (doDestroy)
            {
                destroyProjectile(true);
                return;
            }
        }

        if (doDestroy)
        {
            destroyProjectile(false);
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        actOnHitObject(other.gameObject, false);
    }

    private void OnCollisionEnter(Collision other)
    {
        actOnHitObject(other.gameObject, true);
        CleanObject(other);
        GreaseObject(other);
    }

    private void GreaseObject(Collision obj)
    {
        GreasableObject greaseObj = obj.gameObject.GetComponent<GreasableObject>();
        if (greaseObj && gameObject.name.StartsWith("OilProjectile"))
        {
            greaseObj.AddGrease();
        }

    }

    private void CleanObject(Collision obj)
    {
        Clean cleanObj = obj.gameObject.GetComponent<Clean>();
        if (cleanObj && gameObject.name.StartsWith("WaterProjectile"))
        {

            Ray ray = new Ray(obj.contacts[0].point + obj.contacts[0].normal, -obj.contacts[0].normal * 5f);
            //Debug.DrawRay(obj.contacts[0].point + obj.contacts[0].normal, -obj.contacts[0].normal * 5f, UnityEngine.Color.blue, 7, false);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                cleanObj.CleanArea(hit.textureCoord);
            } 
        }
    }

}
