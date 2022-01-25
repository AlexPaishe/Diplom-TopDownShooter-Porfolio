using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private CameraMainMenuScript cam;
    private Animator anima;

    void Start()
    {
        cam = FindObjectOfType<CameraMainMenuScript>();
        anima = GetComponent<Animator>();
    }

    /// <summary>
    /// Переход на другое меню
    /// </summary>
    public void NewStep()
    {
        cam.GoTrue();
        anima.SetBool("Start", true);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Выход из окна меню
    /// </summary>
    /// <param name="number"></param>
    public void End(int number)
    {
        anima.SetBool("Start", false);
        cam.NumberPoint(number);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
