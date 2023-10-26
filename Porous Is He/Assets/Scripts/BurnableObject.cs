using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private FlameScript flameScript;

    void Start()
    {
        flameScript.SetIsBurning(false);
    }

    void Update()
    {
        if (flameScript.fireLevel == flameScript.maxFireLevel && flameScript.GetIsBurning())
        {
            Invoke("BurnObject", 2);
        }
    }

    public void BurnObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PoCombust.isOnFire && flameScript.fireLevel > 0)
            {
                flameScript.SetIsBurning(true);
            }
        }
    }
}
