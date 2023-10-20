using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private int levelUnlocked;
    private AudioManager AudioManager;
    private bool audioInit = false;

    public bool isPaused; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            audioInit = true;
            Destroy(this);
        }
    }

    void Start()
    {
        levelUnlocked = 1;
        AudioManager = this.GetComponent<AudioManager>();

        if (!audioInit)
        {
            AudioManager.PlayMainMenu();
            SceneManager.sceneLoaded += OnSceneLoaded;
            audioInit = true;
        }
    }

    void Update()
    {
        
    }

    public void LevelCompleted(int level)
    {
        levelUnlocked = level+1 > levelUnlocked ? level + 1 : levelUnlocked;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "MainMenu" || scene.name == "LevelSelect")
        {
            AudioManager.PlayMainMenu();
        } else
        {
            AudioManager.NoMusic();
        }
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }
    /*    public void Pause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
        }



        public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }*/


}
