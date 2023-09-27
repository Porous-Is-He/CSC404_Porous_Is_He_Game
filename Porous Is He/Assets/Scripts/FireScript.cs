using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireScript : MonoBehaviour
{
    //public GameObject spawnObject;
    //public GameObject objectClone;

    public bool isOnFire = false;

    // Start is called before the first frame update
    void Start()
    {
        //Transform spawnLocation = this.transform;
        //GameObject spawnObject = GameObject.Find("Particle System");
        //GameObject objectClone = GameObject.Find("Particle System");
        //Instantiate(spawnObject, spawnLocation.transform.position, Quaternion.Euler(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnFire == true)
        {

        } else
        {

        }
    }

    public void Begin()
    {
        if (isOnFire != true)
        {
            isOnFire = true;
            this.transform.Find("FireModel").gameObject.SetActive(true);
        }
    }

    public bool IsOnFire()
    {
        return isOnFire;
    }

    public void Stop()
    {
        isOnFire = false;
        this.transform.Find("FireModel").gameObject.SetActive(false);

        //Go to the "You have completed the demo" Scene
        SceneManager.LoadScene("GameOver");
    }

    //private void OnParticleCollision(GameObject other)
    //{
    //    if (collision.gameObject.tag == "Water")
    //    {
    //        Stop();

    //        //if want water drop to be gone
    //        //Destroy(this.gameObject);
    //        KeepScore.Score += 1;
    //    }
    //}
}
