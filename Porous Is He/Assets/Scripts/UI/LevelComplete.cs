using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private GameObject LevelCompleteUI;
    public string NextLevelScene;

    void Start()
    {
       LevelCompleteUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        PauseMenu.isPaused = true;
        LevelCompleteUI.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(NextLevelScene);
    }

}
