using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteMenuScript : MonoBehaviour
{

    private LevelComplete LvlComplete;

    // Start is called before the first frame update
    void Start()
    {
        LvlComplete = GameObject.Find("GoalRegion").GetComponent<LevelComplete>();
    }

    public void NextLevel()
    {
        LvlComplete.NextLevel();
    }
    public void MainMenu()
    {
        LvlComplete.MainMenu();
    }
}
