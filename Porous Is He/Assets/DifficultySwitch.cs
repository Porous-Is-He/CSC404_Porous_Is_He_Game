using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySwitch : MonoBehaviour
{
    private DifficultyManager diffm;
    public bool appearOnHard;
    public bool appearOnNormal;

    // Start is called before the first frame update
    void Start()
    {
        diffm = GameObject.Find("GameController").GetComponent<DifficultyManager>();
        diffm.loadedEvent.AddListener(AssignExistence);
    }

    void AssignExistence()
    {
        gameObject.SetActive(false);
        if (appearOnHard == true && diffm.isHardmode == true)
        {
            gameObject.SetActive(true);
        }
        if (appearOnNormal == true && diffm.isHardmode == false)
        {
            gameObject.SetActive(true);
        }
    }
}
