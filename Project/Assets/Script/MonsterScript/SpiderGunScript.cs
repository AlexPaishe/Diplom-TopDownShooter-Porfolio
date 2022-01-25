using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderGunScript : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _speed;
    [SerializeField] private float _timerBullet;
    [SerializeField] private float _distance;
    [SerializeField] private HealthSpiderScript _spider;
    private float _currentTime;

    private ShootingScript _player;

    void Start()
    {
        _player = FindObjectOfType<ShootingScript>();
        _currentTime = _timerBullet;
    }

    void Update()
    {
        _currentTime -= Time.deltaTime;
        if(Vector3.Distance(_player.transform.position, transform.position) < _distance)
        {
            if (_player.gameObject.layer == 10)
            {
                if (_spider.Alive() == true)
                {
                    if (_currentTime < 0)
                    {
                        GameObject bullets = Instantiate(_bullet, transform.position, transform.rotation);
                        bullets.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.right * _speed);
                        _currentTime = _timerBullet;
                    }
                }
            }
        }
    }
}
