using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour
{
    public AudioSource GameMusic;

    private void Start()
    {
        GameMusic.enabled = true;
            GameMusic.mute = false;
            GameMusic.loop = true;
            GameMusic.Play();
    }


}

