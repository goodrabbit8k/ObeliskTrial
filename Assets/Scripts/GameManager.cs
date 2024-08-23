using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool puzzleStart = false;
    public bool winGame;
    public bool gameOver;
    public bool pause;

    [SerializeField] GameObject finishLine;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject backgroundPause;

    [SerializeField] float loadLevelDelay = 1f;
    [SerializeField] float gameOverDelay = 2f;

    [SerializeField] GameObject[] deletedWays;
    [SerializeField] GameObject[] turnOnCells;

    void Update() 
    {
        if (puzzleStart) 
        {
            finishLine.SetActive(true);

            if (deletedWays != null)
            {
                for (int i = 0; i < deletedWays.Length; i++)
                {
                    Destroy(deletedWays[i]);
                }
            }

            if (turnOnCells != null)
            {
                for (int i = 0; i < turnOnCells.Length; i++)
                {
                    turnOnCells[i].SetActive(true);
                }
            }
            
            if (FindObjectOfType<CameraShake>() != null) 
            {
                FindObjectOfType<CameraShake>().startShaking = true;
            }
        }

        if (winGame) 
        {
            StartCoroutine(LoadNextLevel());
        }

        if (gameOver)
        {
            gameOverDelay -= 1 * Time.deltaTime;

            if (gameOverDelay <= 0) 
            {
                gameOverDelay = 0f;
                gameOverMenu.SetActive(true);
            }
        }

        if (pause) 
        {
            pauseMenu.SetActive(true);
            pauseButton.SetActive(false);
            backgroundPause.SetActive(true);

            FindObjectOfType<WaySelection>().canClick = false;
        }    
        else 
        {
            pauseMenu.SetActive(false);
            pauseButton.SetActive(true);
            backgroundPause.SetActive(false);

            FindObjectOfType<WaySelection>().canClick = true;
        }
    }

    IEnumerator LoadNextLevel() 
    {
        yield return new WaitForSecondsRealtime(loadLevelDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
