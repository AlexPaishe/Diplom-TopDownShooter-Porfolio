using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private bool _ghostBullet;

    private void OnTriggerEnter(Collider other)
    {
        if (_ghostBullet == false)
        {
            if (other.CompareTag("Enemy"))
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
                else if(other.gameObject.GetComponent<DamageTrainingScript>() != null)
                {
                    other.gameObject.GetComponent<DamageTrainingScript>().TakeDamage(_damage);
                }
                else if (other.gameObject.GetComponent<DamageDaymanScript>() != null)
                {
                    other.gameObject.GetComponent<DamageDaymanScript>().TakeDamage(_damage);
                }
                else if (other.gameObject.GetComponent<DamageSpiderScript>() != null)
                {
                    other.gameObject.GetComponent<DamageSpiderScript >().TakeDamage(_damage);
                }
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Enemy"))
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
                else if(other.gameObject.GetComponent<DamageTrainingScript>() != null)
                {
                    other.gameObject.GetComponent<DamageTrainingScript>().TakeDamage(_damage);
                }
                else if (other.gameObject.GetComponent<DamageDaymanScript>() != null)
                {
                    other.gameObject.GetComponent<DamageDaymanScript>().TakeDamage(_damage);
                }
                else if (other.gameObject.GetComponent<DamageSpiderScript>() != null)
                {
                    other.gameObject.GetComponent<DamageSpiderScript>().TakeDamage(_damage);
                }
            }
            else if(other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }
}
