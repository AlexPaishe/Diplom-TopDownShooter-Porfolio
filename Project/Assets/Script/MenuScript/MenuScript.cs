using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    /// <summary>
    /// Перезагрузка уровня
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Возвращение в главное меню
    /// </summary>
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Выключить меню паузы
    /// </summary>
    public void Return()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}
