using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeScript : MonoBehaviour
{
    bool lvl2Avalible = false;
    public Button activeLVL2;
    public Text textOnButton;

    public Text curMachines;
    public Text curMoney;

    public static int Machines;
    public float money;


    public void Start()
    {
    }

    public void Update()
    {
        money = CoinsScript.coins;
        curMoney.text = money.ToString();

        curMachines.text = Machines.ToString();
        
        if (Machines >= 10 && money >= 100 && SceneManager.GetActiveScene().name == "FirstLevel")
        {
            
            textOnButton.color = Color.green;
        }
        else
        {
            textOnButton.color = Color.red;
            

        }

        if (SceneManager.GetActiveScene().name == "SecondLevel")
        {
            if (Machines >= 20 && money >= 200)
            {
                textOnButton.color = Color.green;
            }
            else
            {
                textOnButton.color = Color.red;


            }
        }
        
    }

    public void ExpandLVL2() 
    {
        if (Machines >= 10 && money >= 100)
        {
            PlayerPrefs.SetInt("Level", 2);
            CoinsScript.coins -= 100;
            Machines = 0;
            CoinsScript.coins += PlayerPrefs.GetFloat("LoadData", CoinsScript.coins);
            PlayerPrefs.SetFloat("LoadData", 0);
            SceneManager.LoadScene("SecondLevel");
            

        }


    }

    public void ExpandLVL3() 
    {
        if (Machines >= 20 && money >= 200)
        {
            PlayerPrefs.SetInt("Level", 3);
            CoinsScript.coins -= 200;
            Machines = 0;
            CoinsScript.coins += PlayerPrefs.GetFloat("LoadData", CoinsScript.coins);
            PlayerPrefs.SetFloat("LoadData", 0);
            SceneManager.LoadScene("ThirdLevel");
        }
    }

    public static void LoadScene() 
    {
        if (PlayerPrefs.GetInt("Level") == 1)
        {
            SceneManager.LoadScene("FirstLevel");
        }
        else if (PlayerPrefs.GetInt("Level") == 2)
        {
            SceneManager.LoadScene("SecondLevel");
        }
        else if (PlayerPrefs.GetInt("Level") == 3)
        {
            SceneManager.LoadScene("ThirdLevel");
        }
    }

}
