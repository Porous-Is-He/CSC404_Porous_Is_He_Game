using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelSelectMenu : MonoBehaviour
{

    public string levelName;

    private void Start()
    {
        // Change to "Level0" once the tutorial level is implemented
        if (levelName == "Tutorial")
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(levelName);
        GameObject.Find("SelectBoard").GetComponent<BubblesInLevelSelect>().sceneIsSwitched = true;
    }

}
