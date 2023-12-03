using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using static BubblesInLevelSelect;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private GameObject LevelCompletePanel;
    [SerializeField] private GameObject LevelCompletePopup;
    [SerializeField] private GameObject selectFirst;
    private Animator animator;
    public string NextLevelScene;
    public TextMeshProUGUI BubblesCollected;
    private TextMeshProUGUI BubblesCollectedForLevel;
    private int allBubbles;
    public static bool LevelEnd;

    void Start()
    {
        LevelCompletePanel.SetActive(false);
        LevelEnd = false;
        animator = LevelCompletePopup.GetComponent<Animator>();
        allBubbles = GameObject.FindGameObjectsWithTag("Bubble").Length;

        Scene scene = SceneManager.GetActiveScene();
        // Check the scene's name and set up the Bubbles GUI correctly
        if (scene.name == "Level1")
        {
            BubblesCollectedForLevel = GameObject.Find("SelectBoard").GetComponent<BubblesInLevelSelect>().Level1Bubbles;
        }
        else if (scene.name == "Level2")
        {
            BubblesCollectedForLevel = GameObject.Find("SelectBoard").GetComponent<BubblesInLevelSelect>().Level2Bubbles;
        }
        else if (scene.name == "Level3")
        {
            BubblesCollectedForLevel = GameObject.Find("SelectBoard").GetComponent<BubblesInLevelSelect>().Level2Bubbles;
        }
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

            BubblesCollectedForLevel.text = other.gameObject.GetComponent<BubbleCountingScript>().bubbles.ToString() + "/" + allBubbles.ToString();

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
