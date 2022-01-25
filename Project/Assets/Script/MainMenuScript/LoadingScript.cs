using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private GameObject _loadscreen;
    [SerializeField] private Slider _barLoad;
    [SerializeField] private Text[] _loadText;
    [SerializeField] private string[] _loadString;
    [SerializeField] private string _loadScenes;
    [SerializeField] private GameObject[] _weaponImage;
    [SerializeField] private float _timer;
    [SerializeField] private Color[] _col;
    [SerializeField] private Image _fill;

    private int loadstep = -1;
    private float currentTimer;
    private AsyncOperation asyncLoad;
    private int rand;

    private void Awake()
    {
        currentTimer = _timer;
        rand = Random.Range(0, _weaponImage.Length);
        _weaponImage[rand].SetActive(true);
        _fill.color = _col[rand];
        _loadText[0].color = _col[rand];
        _loadText[1].color = _col[rand];
        int scene = PlayerPrefs.GetInt("Scene");
        if (scene == 0)
        {
            PlayerPrefs.SetInt("Scene", 1);
            SceneManager.LoadScene(0);
        }
        else if (scene == 1)
        {
            PlayerPrefs.SetInt("Scene", 0);
        }
    }

    private void Update()
    {
        if (asyncLoad != null)
        {
            _barLoad.value = asyncLoad.progress;
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                loadstep++;
                if (loadstep == _loadString.Length)
                {
                    loadstep = 0;
                }
                _loadText[0].text = _loadString[loadstep];
                currentTimer = _timer;
            }
            if (asyncLoad.progress >= .9f && !asyncLoad.allowSceneActivation)
            {
                _loadText[0].enabled = false;
                _loadText[1].enabled = true;
                _loadText[1].text = "Нажмите любую кнопку";
                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
        }
    }

    /// <summary>
    /// Меню загрузки игры
    /// </summary>
    public void Load()
    {
        _loadscreen.SetActive(true);
        asyncLoad = SceneManager.LoadSceneAsync(_loadScenes);
        asyncLoad.allowSceneActivation = false;
    }
}
