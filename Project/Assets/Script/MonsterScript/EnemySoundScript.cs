using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemySoundScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] _conditions;
    private AudioSource _sound;
    private InterfaceScript _inter;
    private bool _pause = false;

    private void Start()
    {
        _sound = GetComponent<AudioSource>();
        _inter = FindObjectOfType<InterfaceScript>();
    }

    private void Update()
    {
        bool fire = _inter.PauseReturn();
        if(fire == true && _pause == false)
        {
            _sound.Pause();
            _pause = true;
        }
        else if(fire == false && _pause == true)
        {
            _sound.Play();
            _pause = false;
        }
    }

    /// <summary>
    /// Реализация звуков смерти и движения
    /// </summary>
    /// <param name="number"></param>
    public void ConditionSound(int number)
    {
        if(_sound.isPlaying)
        {
            _sound.Stop();
        }
        _sound.clip = _conditions[number];
        if(number == 0)
        {
            _sound.loop = true;
            _sound.playOnAwake = true;
        }
        else
        {
            _sound.loop = false;
        }
        _sound.Play();
    }
}
