using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip menu;
    private AudioSource Audio;
    bool play;

    void Start()
    {
        play = true;
        Audio = GetComponent<AudioSource>();
        Audio.enabled = true;
        Audio.clip = menu;
        Audio.loop = true;
        Audio.Play();
    }



}
