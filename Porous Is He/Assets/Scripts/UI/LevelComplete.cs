using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private GameObject LevelCompletePanel;
    [SerializeField] private GameObject LevelCompletePopup;
    [SerializeField] private GameObject selectFirst;
    private Animator animator;
    public string NextLevelScene;
    public TextMeshProUGUI BubblesCollected;
    private int allBubbles;
    public static bool LevelEnd;

    void Start()
    {
        LevelCompletePanel.SetActive(false);
        LevelEnd = false;
        animator = LevelCompletePopup.GetComponent<Animator>();
        allBubbles = GameObject.FindGameObjectsWithTag("Bubble").Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            PauseMenu.isPaused = true;
            LevelCompletePanel.SetActive(true);
            BubblesCollected.text = "Bubbles Collected: " + other.gameObject.GetComponent<BubbleCountingScript>().bubbles.ToString() + "/" + allBubbles.ToString();
            EventSystem.current.SetSelectedGameObject(selectFirst);

            if (animator != null && animator.isActiveAndEnabled)
            {
                animator.SetBool("open", true);
                StartCoroutine(WaitAnimationEnd());
            }
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
        if (NextLevelScene == "MainMenu")
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(NextLevelScene);
    }

}
