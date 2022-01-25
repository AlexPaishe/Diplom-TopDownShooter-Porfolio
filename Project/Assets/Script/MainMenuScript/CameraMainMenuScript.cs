using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMainMenuScript : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float [] _speed;
    [SerializeField] private GameObject[] _menu;
    [SerializeField] private Transform _magicPlane;
    [SerializeField] private Transform[] _magicPoint;
    [SerializeField] private Text _recordText;
    [Header("Portal")]
    [SerializeField] private GameObject _portal;

    private TrainingShootScript training;
    private Vector3 Target;
    private int number = 0;
    private bool go;
    private bool forward;
    private float speedCam;
    private bool back = false;

    private void Awake()
    {
        Target =_points[number].position;
        go = true;
        forward = false;
        Time.timeScale = 1;
        training = FindObjectOfType<TrainingShootScript>();
        int record = PlayerPrefs.GetInt("Record");
        _recordText.text = $"  Record : {record}";
    }

    void Update()
    {
        if (go == true)
        {
            if(transform.position.y == Target.y)
            {
                speedCam = _speed[1] * Time.deltaTime;
            }
            else if( transform.position.y != Target.y)
            {
                speedCam = _speed[number] * Time.deltaTime;
            }

            transform.position = Vector3.MoveTowards(transform.position, Target, speedCam);
            if(transform.position == Target)
            {
                go = false;
                if (number == 0 || number == 1)
                {
                    _menu[number].SetActive(true);
                }
                else if(number == 2)
                {
                    CameraDown();
                }
                else if(number == 3)
                {
                    _portal.SetActive(true);
                }
            }
        }

        if(forward == false && _magicPlane.position != _magicPoint[0].position)
        {
            _magicPlane.position = Vector3.MoveTowards(_magicPlane.position, _magicPoint[0].position, _speed[2] * Time.deltaTime);
        }
        else if (forward == true && _magicPlane.position != _magicPoint[1].position)
        {
            _magicPlane.position = Vector3.MoveTowards(_magicPlane.position, _magicPoint[1].position, _speed[2] * Time.deltaTime);
        }
        else if(back == true && forward == false && _magicPlane.position == _magicPoint[0].position)
        {
            back = false;
            transform.position = _points[2].position;
            transform.rotation = Quaternion.Euler(new Vector3(10, 180, 0));
            NumberPoint(0);
            go = true;
            _menu[3].SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && forward == true && _magicPlane.position == _magicPoint[1].position)
        {
            CameraUp();
        }
    }

    /// <summary>
    /// Реализация перехода на следующее меню
    /// </summary>
    /// <param name="numb"></param>
    public void NumberPoint(int numb)
    {
        number = numb;
        Target = _points[number].position;
    }

    /// <summary>
    /// Реализация перехода в полигон
    /// </summary>
    private void CameraDown()
    {
        transform.position = _magicPoint[2].position;
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        forward = true;
        _menu[2].SetActive(true);
        _menu[3].SetActive(false);
    }

    /// <summary>
    /// Реализация выхода из полигона
    /// </summary>
    public void CameraUp()
    {
        forward = false;
        back = true;
        _menu[2].SetActive(false);
        training.Stands();
    }

    /// <summary>
    /// Реализация старта передвижения меню
    /// </summary>
    public void GoTrue()
    {
        go = true;
    }

    /// <summary>
    /// Находится ли Игрок на тренировке
    /// </summary>
    /// <returns></returns>
    public bool ForwardBool()
    {
        return forward;
    }
}
