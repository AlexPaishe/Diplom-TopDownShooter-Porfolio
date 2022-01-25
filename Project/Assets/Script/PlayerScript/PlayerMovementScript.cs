using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody)),
    RequireComponent(typeof(AudioSource))]
public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float _speed;
    [SerializeField] private Animator _anima;
    [SerializeField] private GameObject _legs;

    private AudioSource _sound;
    private InterfaceScript _inter;
    private bool _pause = false;
    private bool _go = false;
    private Camera _cam;

    private Vector3 _LookLegs;
   
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _sound = GetComponent<AudioSource>();
        _inter = FindObjectOfType<InterfaceScript>();
        _cam = Camera.main;
    }

    private void FixedUpdate()
    {
        float ver = Input.GetAxisRaw("Vertical");
        float hor = Input.GetAxisRaw("Horizontal");
        Move(ver, hor);
    }

    private void Update()
    {
        FallenAndPause();
    }

    /// <summary>
    /// Передвижение главного героя
    /// </summary>
    /// <param name="vertical"></param>
    /// <param name="horizontal"></param>
    private void Move(float vertical, float horizontal)
    {
        if (_pause == false)
        {
            Vector3 forwardVec = new Vector3(_cam.transform.forward.x * 2, 0, _cam.transform.forward.z * 2);
            Vector3 rightVec = new Vector3(_cam.transform.right.x, 0, _cam.transform.right.z);
            Vector3 Vec = new Vector3((rightVec.x * horizontal) + (forwardVec.x * vertical), 0, (rightVec.z * horizontal) + (forwardVec.z * vertical));
            _rb.AddForce(new Vector3(Vec.x * _speed, 0, Vec.z * _speed));
            if (horizontal != 0 || vertical != 0)
            {
                _anima.speed = 1.6f;
                if (_go == false)
                {
                    _go = true;
                    _sound.Play();
                    _sound.pitch = _anima.speed;
                }
            }
            else if (horizontal == 0 && vertical == 0)
            {
                _anima.speed = 1f;
                if (_go == true)
                {
                    _go = false;
                    _sound.Pause();
                }
            }

            int run = 0;
            if (horizontal != 0 || vertical != 0)
            {
                _LookLegs = _legs.transform.position;
                _LookLegs.x += Vec.x;
                _LookLegs.z += Vec.z;
                _legs.transform.LookAt(_LookLegs);
                run = 1;
            }
            else if (horizontal == 0 && vertical == 0)
            {
                run = 0;
            }
            _anima.SetInteger("Run", run);
        }
    }

    /// <summary>
    /// Отключение и включение звуков шагов при падении и паузе
    /// </summary>
    private void FallenAndPause()
    {
        bool fire = _inter.PauseReturn();
        if (fire == true && _pause == false)
        {
            _pause = true;
            _go = false;
            _sound.Pause();
        }
        else if (fire == false && _pause == true)
        {
            _pause = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            switch(other.name)
            {
                case "BackWall":
                    _rb.velocity = Vector3.forward * 4;
                    break;
                case "ForwardWall":
                    _rb.velocity = -Vector3.forward * 4;
                    break;
                case "RightWall":
                    _rb.velocity = Vector3.right * 4;
                    break;
                case "LeftWall":
                    _rb.velocity = -Vector3.right * 4;
                    break;
            }
        }
    }
}
