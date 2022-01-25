using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class BizierBullet : MonoBehaviour
{
    [SerializeField] private Transform[] _point;
    [SerializeField] private GameObject _parent;
    [SerializeField] private int _step = 0;
    [SerializeField] private float _t;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    /// <summary>
    /// Реализация кривой Безье
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    private Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 p01 = Vector3.Lerp(p0, p1, t);
        Vector3 p12 = Vector3.Lerp(p1, p2, t);
        Vector3 p23 = Vector3.Lerp(p2, p3, t);

        Vector3 p012 = Vector3.Lerp(p01, p12, t);
        Vector3 p123 = Vector3.Lerp(p12, p23, t);

        Vector3 p0123 = Vector3.Lerp(p012, p123, t);

        return p0123;
    }

    private void Update()
    {
        BezierLine();
    }

    private void OnTriggerEnter(Collider other)
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
            else if (other.gameObject.GetComponent<DamageDaymanScript>() != null)
            {
                other.gameObject.GetComponent<DamageDaymanScript>().TakeDamage(_damage);
            }
            else if (other.gameObject.GetComponent<DamageSpiderScript>() != null)
            {
                other.gameObject.GetComponent<DamageSpiderScript>().TakeDamage(_damage);
            }
            else if (other.gameObject.GetComponent<DamageTrainingScript>() != null)
            {
                other.gameObject.GetComponent<DamageTrainingScript>().TakeDamage(_damage);
            }
            Destroy(_parent);
        }
        else
        {
            Destroy(_parent);
        }
    }

    /// <summary>
    /// Реализация пути состоящий из кривых Безье
    /// </summary>
    private void BezierLine()
    {
        if ((_step * 3) + 3 < _point.Length)
        {
            _t += Time.deltaTime * _speed;
            transform.position = GetPoint(_point[(_step * 3)].position, _point[(_step * 3) + 1].position,
                _point[(_step * 3) + 2].position, _point[(_step * 3) + 3].position, _t);
            if (_t > 1)
            {
                _step++;
                _t = 0;
            }
        }
        else
        {
            Destroy(_parent);
        }
    }
}
