using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private GameObject LevelCompletePanel;
    [SerializeField] private GameObject LevelCompletePopup;
    private Animator animator;
    public string NextLevelScene;

    public static bool LevelEnd;

    void Start()
    {
        LevelCompletePanel.SetActive(false);
        LevelEnd = false;
        animator = LevelCompletePopup.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
        PauseMenu.isPaused = true;
        LevelCompletePanel.SetActive(true);

        if (animator != null )
        {
            animator.SetBool("open", true);
            StartCoroutine(WaitAnimationEnd());
        }
    }

    IEnumerator WaitAnimationEnd()
    {
        while (!AnimatorFinished())
        {
            yield return null;
        }
        Time.timeScale = 0f;
        LevelEnd = true;
    }

    bool AnimatorFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(NextLevelScene);
    }

}
