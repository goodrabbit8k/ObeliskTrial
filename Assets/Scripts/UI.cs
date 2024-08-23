using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject creditMenu;

    public void PlayButton() 
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseButton() 
    {
        FindObjectOfType<GameManager>().pause = true;
        Time.timeScale = 0;
    }

    public void ResumeButton() 
    {
        FindObjectOfType<GameManager>().pause = false;
        Time.timeScale = 1;
    }

    public void HomeButton() 
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void CreditButton()
    {
        mainMenu.SetActive(false);
        creditMenu.SetActive(true);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        creditMenu.SetActive(false);
    }
}
