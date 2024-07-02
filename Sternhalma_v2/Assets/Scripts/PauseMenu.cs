using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool gameIsPaused = false;

    private void Awake()
    {
        //pauseMenu.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Tab pressed!");
            Debug.Log(gameIsPaused);
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Debug.Log("game is Paused = true!");
                Pause();
            }

        }
    }

    public void onClickPause()
    {
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        gameIsPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        gameIsPaused = false;
        SceneManager.LoadScene("MainMenu");

    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
