using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public static bool isPaused;

    private PlayerInputActions playerInputActions;

    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();

        playerInputActions = new PlayerInputActions();
        playerInputActions.GameManager.Enable();
        playerInputActions.GameManager.Pause.started += PauseGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (LevelComplete.LevelEnd) return;
        if (isPaused)
        {
            ResumeGame();
        } else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }



    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        playerInputActions.GameManager.Disable();
    }
}
