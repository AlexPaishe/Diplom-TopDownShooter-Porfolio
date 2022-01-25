using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceScript : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settingMenu;
    [SerializeField] private GameObject [] _quitMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private Text _scoreText;
    [SerializeField] private AudioMixerGroup [] _mixer;
    [SerializeField] private Slider[] _sliders;
    [SerializeField] private string[] _nameSliders;
     
    private int scorenumber = 0;
    private bool pause = false;
    private bool gameover = false;
    private AudioSource click;

    private void Awake()
    {
        Time.timeScale = 1;
        _scoreText.text = $"Score: {scorenumber}";
        for(int i = 0; i < _sliders.Length; i++)
        {
            _sliders[i].value = PlayerPrefs.GetFloat(_nameSliders[i]);
        }

        click = GetComponent<AudioSource>();
    }

    private void Start()
    {
        for (int i = 0; i < _sliders.Length; i++)
        {
            _mixer[i].audioMixer.SetFloat(_nameSliders[i], PlayerPrefs.GetFloat(_nameSliders[i]));
        }
    }

    private void Update()
    {
        Pause();
    }

    /// <summary>
    /// Включение и выключение меню паузы
    /// </summary>
    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseMenu.activeSelf && pause == true && gameover == false || _settingMenu.activeSelf && pause == true && gameover == false)
            {
                _pauseMenu.SetActive(false);
                _settingMenu.SetActive(false);
                Time.timeScale = 1;
                pause = false;
            }
            else if (pause == false && _pauseMenu.activeSelf == false && gameover == false)
            {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0;
                pause = true;
            }
        }
    }

    /// <summary>
    /// Получение очков за гибель монстров
    /// </summary>
    /// <param name="score"></param>
    public void Score(int score)
    {
        scorenumber += score;
        _scoreText.text = $"Score: {scorenumber}";
    }

    /// <summary>
    /// На паузе ли игра
    /// </summary>
    /// <returns></returns>
    public bool PauseReturn()
    {
        return pause;
    }

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
        pause = false;
        _pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Включение и выключение меню настроек
    /// </summary>
    public void Setting()
    {
        if(_pauseMenu.activeSelf)
        {
            _settingMenu.SetActive(true);
            _pauseMenu.SetActive(false);
        }
        else if(_pauseMenu.activeSelf == false)
        {
            _pauseMenu.SetActive(true);
            _settingMenu.SetActive(false);
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
    }

    /// <summary>
    /// Реализация изменения громкости врагов
    /// </summary>
    /// <param name="val"></param>
    public void SlidersEnemy(float val)
    {
        PlayerPrefs.SetFloat(_nameSliders[2], val);
        _mixer[2].audioMixer.SetFloat(_nameSliders[2], val);
    }

    /// <summary>
    /// Реализация изменения громкости оружия
    /// </summary>
    /// <param name="val"></param>
    public void SlidersWeapon(float val)
    {
        PlayerPrefs.SetFloat(_nameSliders[1], val);
        _mixer[1].audioMixer.SetFloat(_nameSliders[1], val);
    }

    /// <summary>
    /// Реализация клика
    /// </summary>
    public void Click()
    {
        click.Play();
    }

    /// <summary>
    /// Возвращает количество очков
    /// </summary>
    /// <returns></returns>
    public int ScoreNumber()
    {
        return scorenumber;
    }

    /// <summary>
    /// Включение и выключение меню выхода
    /// </summary>
    /// <param name="number"></param>
    public void ExitMenu(int number)
    {
        if(number == 0 && _quitMenu[0].activeSelf == false)
        {
            _quitMenu[0].SetActive(true);
            _pauseMenu.SetActive(false);
        }
        else if(number == 0 && _quitMenu[0].activeSelf == true)
        {
            _pauseMenu.SetActive(true);
            _quitMenu[0].SetActive(false);
        }
        else if (number == 1 && _quitMenu[1].activeSelf == false)
        {
            _quitMenu[1].SetActive(true);
            _gameOverMenu.SetActive(false);
        }
        else if (number == 1 && _quitMenu[1].activeSelf == true)
        {
            _gameOverMenu.SetActive(true);
            _quitMenu[1].SetActive(false);
        }
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Остановка звуков при проигрыше
    /// </summary>
    public void GameOver()
    {
        pause = true;
        gameover = true;
    }
}
