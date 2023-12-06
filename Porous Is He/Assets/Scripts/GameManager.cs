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
    private bool audioInit = false;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
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

        Scene scene = SceneManager.GetActiveScene();
        if (!audioInit)
        {
            if (scene.name == "MainMenu" || scene.name == "LevelSelect")
            {
                AudioManager AudioManager = this.GetComponent<AudioManager>();
                AudioManager.PlayMainMenu();
            }
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
            AudioManager AudioManager = this.GetComponent<AudioManager>();
            AudioManager.PlayMainMenu();
        } else
        {
            AudioManager AudioManager = this.GetComponent<AudioManager>();
            AudioManager.NoMusic(scene.name);
        }
    }
}
