using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
   
    private bool check;
    private bool checkForUp;
    public static bool cameraMove;

    public Image panel;
    public Image UpPanel;


    void Start()
    {
        panel.gameObject.SetActive(false);
        UpPanel.gameObject.SetActive(false);
        check = false;
        checkForUp = false;
        cameraMove = true;
    }
    private void Update()
    {
        
    }

    public void menuDrive()
    {

        if (!check)
        {
            
            PlayerControls.canWalk = false;
            panel.gameObject.SetActive(true);
            check = true;


        }
        else if(check)
        {
            

            panel.gameObject.SetActive(false);
            check = false;
            PlayerControls.canWalk = true;
        }
    }

    public void upgradeMenu()
    {

        if (!checkForUp)
        {
            cameraMove = false;
            PlayerControls.canWalk = false;
            UpPanel.gameObject.SetActive(true);
            checkForUp = true;


        }
        else if (checkForUp)
        {
            cameraMove = true;
            UpPanel.gameObject.SetActive(false);
            checkForUp = false;
            PlayerControls.canWalk = true;
        }
    }
}
