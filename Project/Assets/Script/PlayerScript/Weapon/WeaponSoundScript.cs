using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponSoundScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] _shoots;
    [SerializeField] private AudioClip[] _recharge;

    private AudioSource _weapon;

    private void Awake()
    {
        _weapon = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Реализация звуков выстрела
    /// </summary>
    /// <param name="shoot"></param>
    public void WeaponShoot(int shoot)
    {
        _weapon.clip = _shoots[shoot];
        if(_weapon.isPlaying)
        {
            _weapon.Stop();
        }
        if(shoot == 4)
        {
            _weapon.pitch = 3;
        }
        else
        {
            _weapon.pitch = 1;
        }
        _weapon.Play();
    }

    /// <summary>
    /// Реализация звуков перезарядки
    /// </summary>
    /// <param name="recharge"></param>
    public void WeaponRecharge(int recharge)
    {
        _weapon.clip = _recharge[recharge];
        _weapon.pitch = 1;
        if(_weapon.isPlaying)
        {
            _weapon.Stop();
        }
        _weapon.Play();
    }
}
