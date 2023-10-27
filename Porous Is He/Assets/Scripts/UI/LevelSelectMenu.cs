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
        if (levelName == "Level0")
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
    }

}
