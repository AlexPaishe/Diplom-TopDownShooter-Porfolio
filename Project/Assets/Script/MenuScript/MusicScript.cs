using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] _sounds;

    private AudioSource music;
    private int number;
    private void Awake()
    {
        music = GetComponent<AudioSource>();
        music.clip = _sounds[number];
        music.Play();
    }

    private void Update()
    {
        if(music.isPlaying == false)
        {
            number++;
            if(number == _sounds.Length)
            {
                number = 0;
            }
            music.clip = _sounds[number];
            music.Play();
        }
    }
}
