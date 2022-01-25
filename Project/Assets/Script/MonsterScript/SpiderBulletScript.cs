using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBulletScript : MonoBehaviour
{
    private InterfaceScript _inter;
    private AudioSource _sound;
    private bool _pause = false;

    private void Start()
    {
        _inter = FindObjectOfType<InterfaceScript>();
        _sound = GetComponent<AudioSource>();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
