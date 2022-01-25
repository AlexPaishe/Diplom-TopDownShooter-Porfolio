using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private GameObject _quitMenu;

    /// <summary>
    /// Открытие меню выхода
    /// </summary>
    public void StartPortal()
    {
        _quitMenu.SetActive(true);
    }

    /// <summary>
    /// Закрытие меню выхода
    /// </summary>
    public void EndPortal()
    {
        _quitMenu.SetActive(false);
    }
}
