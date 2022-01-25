using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutionScript : MonoBehaviour
{
    [SerializeField] private Button[] _screenRes;

    private void Awake()
    {
        int width = PlayerPrefs.GetInt("Width");
        int heigh = PlayerPrefs.GetInt("Heigh");
        if(width == 0)
        {
            width = 1920;
        }
        if(heigh == 0)
        {
            heigh = 1080;
        }
        Screen.SetResolution(width, heigh, true);
        if(Screen.width == 1280)
        {
            _screenRes[0].interactable = false;
        }
        else if(Screen.width == 1920)
        {
            _screenRes[1].interactable = false;
        }
        else if (Screen.width == 2000)
        {
            _screenRes[2].interactable = false;
        }
    }

    /// <summary>
    /// Реализация изменения разрешения экрана
    /// </summary>
    /// <param name="number"></param>
    public void Change(int number)
    {
        if (number == 0)
        {
            Screen.SetResolution(1280, 720, true);
            PlayerPrefs.SetInt("Width", 1280);
            PlayerPrefs.SetInt("Heigh", 720);
        }
        else if (number == 1)
        {
            Screen.SetResolution(1920, 1080, true);
            PlayerPrefs.SetInt("Width", 1920);
            PlayerPrefs.SetInt("Heigh", 1080);
        }
        else if (number == 2) 
        { 
            Screen.SetResolution(2000, 1080, true);
            PlayerPrefs.SetInt("Width", 2000);
            PlayerPrefs.SetInt("Heigh", 1080);
        }

        for(int i =0; i< _screenRes.Length; i++)
        {
            if(i == number)
            {
                _screenRes[i].interactable = false;
            }
            else
            {
                _screenRes[i].interactable = true;
            }
        }
    }
}
