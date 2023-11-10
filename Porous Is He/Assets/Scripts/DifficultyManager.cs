using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DifficultyManager : MonoBehaviour
{

    public bool isHardmode = false;

    private PlayerInfoManager pim;
    public bool difficultyAssigned = false;

    public UnityEvent loadedEvent;

    // Start is called before the first frame update
    void Start()
    {
        pim = GameObject.Find("GameController").GetComponent<PlayerInfoManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!difficultyAssigned && pim.ready)
        {
            isHardmode = pim.hardmode;
            difficultyAssigned = true;
            loadedEvent.Invoke();
        }
    }
    bool GetIsHardmode()
    {
        if (!pim.ready)
            Debug.LogError("Save is not loaded");
        return isHardmode;
    }

}
