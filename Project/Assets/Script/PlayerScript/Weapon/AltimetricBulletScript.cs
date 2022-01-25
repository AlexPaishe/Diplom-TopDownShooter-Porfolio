using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AltimetricBulletScript : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speedExplosion;
    [SerializeField] private float _maxRadius;
    [SerializeField] private float _minRadius;

    private Rigidbody _rb;
    private SphereCollider _col;
    private bool _isBoom = false;
    private bool _pause = false;
    private float _forward = 1;
    private AudioSource _explosion;
    private InterfaceScript _inter;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<SphereCollider>();
        _explosion = GetComponent<AudioSource>();
        _inter = FindObjectOfType<InterfaceScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if(collision.collider.GetComponent<HealthScriptMonster>() != null)
            {
                collision.gameObject.GetComponent<HealthScriptMonster>().TakeDamage(_damage);
            }
            else if(collision.collider.GetComponent<HealthDaymonScript>() != null)
            {
                collision.gameObject.GetComponent<HealthDaymonScript>().TakeDamage(_damage);
            }
            else if (collision.collider.GetComponent<HealthSpiderScript>() != null)
            {
                collision.gameObject.GetComponent<HealthSpiderScript>().TakeDamage(_damage);
            }
            else if (collision.collider.GetComponent<DamageTrainingScript>() != null)
            {
                collision.gameObject.GetComponent<DamageTrainingScript>().TakeDamage(_damage);
            }
            else if (collision.collider.GetComponent<DamageDaymanScript>() != null)
            {
                collision.gameObject.GetComponent<DamageDaymanScript>().TakeDamage(_damage);
            }
            else if (collision.collider.GetComponent<DamageSpiderScript>() != null)
            {
                collision.gameObject.GetComponent<DamageSpiderScript>().TakeDamage(_damage);
            }

            if (_isBoom == false)
            {
                _isBoom = true;
                _rb.isKinematic = true;
                _col.isTrigger = true;
                _explosion.Play();
            }
        }
        else
        {
            if (_isBoom == false)
            {
                _rb.isKinematic = true;
                _isBoom = true;
                _col.isTrigger = true;
                _explosion.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<HealthScriptMonster>() != null)
            {
                other.gameObject.GetComponent<HealthScriptMonster>().TakeDamage(_damage);
            }
            else if (other.gameObject.GetComponent<HealthDaymonScript>() != null)
            {
                other.gameObject.GetComponent<HealthDaymonScript>().TakeDamage(_damage);
            }
            else if (other.gameObject.GetComponent<HealthSpiderScript>() != null)
            {
                other.gameObject.GetComponent<HealthSpiderScript>().TakeDamage(_damage);
            }
            else if (other.gameObject.GetComponent<DamageTrainingScript>() != null)
            {
                other.gameObject.GetComponent<DamageTrainingScript>().TakeDamage(_damage);
            }
            else if (other.gameObject.GetComponent<DamageDaymanScript>() != null)
            {
                other.gameObject.GetComponent<DamageDaymanScript>().TakeDamage(_damage);
            }
            else if (other.gameObject.GetComponent<DamageSpiderScript >() != null)
            {
                other.gameObject.GetComponent<DamageSpiderScript >().TakeDamage(_damage);
            }
        }
    }

    private void Update()
    {
        if (_isBoom == true)
        {
            transform.localScale = new Vector3(transform.localScale.x + (_forward * _speedExplosion * Time.deltaTime),
                transform.localScale.y + (_forward * _speedExplosion * Time.deltaTime), transform.localScale.z + (_forward * _speedExplosion * Time.deltaTime));
            if (transform.localScale.x > _maxRadius)
            {
                _forward = -1f;
            }
            else if (transform.localScale.x < _minRadius)
            {
                Destroy(gameObject);
            }

            if (_inter != null)
            {
                bool fire = _inter.PauseReturn();

                if (fire == true && _pause == false)
                {
                    _explosion.Pause();
                    _pause = true;
                }
                else if (fire == false && _pause == true)
                {
                    _explosion.Play();
                    _pause = false;
                }
            }
        }
    }
}
