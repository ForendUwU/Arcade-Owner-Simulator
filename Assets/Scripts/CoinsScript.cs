using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsScript : MonoBehaviour
{
    public static float coins;
    public Text coinsText;

    private string file = "Money";

    bool cheat = false;
    void Start()
    {
        coins = (float)Math.Round(PlayerPrefs.GetFloat(file, coins), 1) + (float)Math.Round(PlayerPrefs.GetFloat("LoadData", coins), 1);
        PlayerPrefs.SetFloat("LoadData", 0);
        coinsText.text = "Money: " + coins + "$";
    }

    private float secundomer;
    void Update()
    {
        coins = (float)Math.Round(coins, 1);
        coinsText.text = "Money: " + coins + "$";
        PlayerPrefs.SetFloat("Money", coins);
        //Debug.Log(coins);


        bool d1 = Input.GetKey(KeyCode.F);
        bool d2 = Input.GetKey(KeyCode.O);
        if (d1 && d2 && !cheat)
        {
            if (secundomer < 5) secundomer += Time.fixedDeltaTime;
            if (secundomer >= 5)
            {
                coins += 1000;
                cheat = true;
            }
            
        }
    }

    //public static void GetCoins(float gotcoins) 
    //{
    //    coins = float.Parse(Math.Round(gotcoins, 2).ToString());
    //    PlayerPrefs.SetFloat("Money", gotcoins);
    //    PlayerPrefs.Save();
    //}

    public static void SaveCoins() 
    {
        PlayerPrefs.SetFloat("Money", coins);
        PlayerPrefs.Save();
    }
}
