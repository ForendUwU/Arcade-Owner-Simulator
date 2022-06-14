using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadGame() 
    {
        UpgradeScript.LoadScene();
    }

    public void NewGame() 
    {

        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("Money");
        PlayerPrefs.DeleteKey("LoadData");
        CoinsScript.coins = 35;
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene("FirstLevel");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
