using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject shopPanel;

    public static bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                shopPanel.SetActive(false);
                Pause();
            }

        }

    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        PlayerControls.canWalk = true;
    }

    public void Pause()
    {
        PlayerControls.canWalk = false;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void BackToMenu() 
    {
        SaveGame();
        SceneManager.LoadScene("Menu");
    }

    public void SaveGame() 
    {
        CoinsScript.SaveCoins();
        PlayerPrefs.Save();
    }
}
