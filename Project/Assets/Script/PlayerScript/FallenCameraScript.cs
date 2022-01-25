using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenCameraScript : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _nextStep = 180;
    private float _currentStep = 180;
    private bool _go = false;

    void Update()
    {
        InputRotate();
    }

    private void FixedUpdate()
    {
        RotateCam();
    }

    /// <summary>
    /// Реализация поворота камеры по клавишам
    /// </summary>
    private void InputRotate()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && _go == false)
        {
            _nextStep -= 90;
            _go = true;
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && _go == false)
        {
            _nextStep += 90;
            _go = true;
        }
    }

    /// <summary>
    /// Реализация поворота камеры
    /// </summary>
    private void RotateCam()
    {
        if (_currentStep == _nextStep && _go == true)
        {
            _currentStep = _nextStep;
            _go = false;
        }
        else if (_currentStep < _nextStep)
        {
            _currentStep += _speed;
            transform.eulerAngles = new Vector3(0, _currentStep, 0);
        }
        else if (_currentStep > _nextStep)
        {
            _currentStep -= _speed;
            transform.eulerAngles = new Vector3(0, _currentStep, 0);
        }
    }
}
