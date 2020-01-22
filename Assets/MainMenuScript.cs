using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void LoadEasy()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void LoadMedium()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }

    public void LoadHardm()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quit the game!");
        Application.Quit();
    }

}
