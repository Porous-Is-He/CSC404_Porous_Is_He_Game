using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplte : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
         Cursor.lockState = CursorLockMode.None;
         SceneManager.LoadScene("GameOver");
    }
}
