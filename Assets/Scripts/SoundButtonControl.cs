using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtonControl : MonoBehaviour
{
    public GameObject Slider;
    bool active = false;
    public void SoundButton()
    {
        if (!active)
        {
            Slider.SetActive(true);
            active = true;
        }
        else {
            Slider.SetActive(false);
            active = false;
        }
        
    }
}
