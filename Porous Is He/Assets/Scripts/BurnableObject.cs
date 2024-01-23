using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private FlameScript flameScript;
    [SerializeField] private DisintegrateController disintegrateController;
    [SerializeField] private float burningTime = 2;
    private bool burned = false;

    void Start()
    {
    }

    void Update()
    {
        if (flameScript.fireLevel == flameScript.maxFireLevel && flameScript.GetIsBurning() && !burned)
        {
            flameScript.isAlwaysBurning = false;
            if (disintegrateController)
            {
                disintegrateController.Dissolve();
            }
            Invoke("BurnObject", burningTime);

            burned = true;
        }
    }

    public void BurnObject()
    {
        flameScript.fireLevel = 0;
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
