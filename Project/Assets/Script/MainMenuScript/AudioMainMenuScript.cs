using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMainMenuScript : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup[] _mixer;
    [SerializeField] private Slider[] _sliders;
    [SerializeField] private string[] _nameSliders;
    [SerializeField] private Transform[] _sphere;
    [SerializeField] private Transform[] _Stands;

    private AudioSource _click;

    private void Awake()
    {
        for (int i = 0; i < _sliders.Length; i++)
        {
            _sliders[i].value = PlayerPrefs.GetFloat(_nameSliders[i]);
            _sphere[i].localScale = Vector3.one;
            _sphere[i].localScale *= (_sliders[i].value + 30) / 50;
        }

        _click = GetComponent<AudioSource>();
    }

    void Start()
    {
        for (int i = 0; i < _sliders.Length; i++)
        {
            _mixer[i].audioMixer.SetFloat(_nameSliders[i], PlayerPrefs.GetFloat(_nameSliders[i]));
        }
    }

    private void Update()
    {
        for(int i = 0; i < _sphere.Length; i++)
        {
            _sphere[i].Rotate(0, 90 * Time.deltaTime, 0);
        }

        for(int i = 0; i < _Stands.Length; i++)
        {
            if(i % 2 == 0)
            {
                _Stands[i].Rotate(0, 0, 90 * Time.deltaTime);
            }
            else if(i % 2 == 1)
            {
                _Stands[i].Rotate(0, 0, -1 * (90 * Time.deltaTime));
            }
        }
    }

    /// <summary>
    /// Реализация изменения громкости музыки
    /// </summary>
    /// <param name="val"></param>
    public void SlidersMusic(float val)
    {
        PlayerPrefs.SetFloat(_nameSliders[0], val);
        _mixer[0].audioMixer.SetFloat(_nameSliders[0], val);
        _sphere[0].localScale = Vector3.one;
        _sphere[0].localScale *= (val + 30) / 50;
    }

    /// <summary>
    /// Реализация изменения громкости врагов
    /// </summary>
    /// <param name="val"></param>
    public void SlidersEnemy(float val)
    {
        PlayerPrefs.SetFloat(_nameSliders[2], val);
        _mixer[2].audioMixer.SetFloat(_nameSliders[2], val);
        _sphere[2].localScale = Vector3.one;
        _sphere[2].localScale *= (val + 30) / 50;
    }

    /// <summary>
    /// Реализация изменения громкости оружия
    /// </summary>
    /// <param name="val"></param>
    public void SlidersWeapon(float val)
    {
        PlayerPrefs.SetFloat(_nameSliders[1], val);
        _mixer[1].audioMixer.SetFloat(_nameSliders[1], val);
        _sphere[1].localScale = Vector3.one;
        _sphere[1].localScale *= (val + 30) / 50;
    }

    /// <summary>
    /// Реализация клика клавишы
    /// </summary>
    public void Click()
    {
        _click.Play();
    }
}
