using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;

    [Header("Audio Clips")]
    public AudioClip mainmenu;

    private string currentScene;


    void Start()
    {
    }

    void Update()
    {
        
    }

    public void PlayMainMenu()
    {
        if (currentScene == "MainMenu") return;
        PlayMusic(mainmenu, "MainMenu");
    }


    private void PlayMusic(AudioClip audio, string sceneName)
    {
        musicSource.Stop();
        musicSource.clip = audio;
        musicSource.loop = true;
        musicSource.Play();
        currentScene = sceneName;
    }

    public void NoMusic(string sceneName)
    {
        if (musicSource)
        {
            musicSource.Stop();
        }
        currentScene = sceneName;
    }
}
