using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FishBezier : MonoBehaviour
{
    [SerializeField] private Transform[] _point;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _gameOver;

    [Range(0, 1)]
    [SerializeField] private float _t;

    private bool go = false;
    private bool stop = false;
    private AudioSource sound;
    private InterfaceScript inter;
    private RecordScript record;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        sound.Stop();
        inter = FindObjectOfType<InterfaceScript>();
        record = FindObjectOfType<RecordScript>();
    }

    void Update()
    {
        if (go == true)
        {
            _t += Time.deltaTime * _speed;
            transform.position = GetPoint(_point[0].position, _point[1].position, _point[2].position, _point[3].position, _t);
            transform.rotation = Quaternion.LookRotation(GetFirstDerivative(_point[0].position, _point[1].position, _point[2].position, _point[3].position, _t));
            if(_t > 1 && stop == false)
            {
                _gameOver.SetActive(true);
                stop = true;
                Time.timeScale = 0;
                sound.Stop();
                inter.GameOver();
                record.Record();
            }
        }
    }

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

    /// <summary>
    /// Реализация поворота кривой Безье
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    private Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1 - p0) + 6f * oneMinusT * t * (p2 - p1) + 3f * t * t * (p3 - p2);
    }   
    
    private void OnDrawGizmos()
    {
        int sigment = 20;
        Vector3 prevouse = _point[0].position;

        for(int i = 0; i< sigment + 1; i++)
        {
            float parametr = (float)i / 20;
            Vector3 points = GetPoint(_point[0].position, _point[1].position, _point[2].position, _point[3].position, parametr);
            Gizmos.DrawLine(prevouse, points);
            prevouse = points;
        }
    }

    /// <summary>
    /// Реализует вылет головы и конец игры
    /// </summary>
    public void Go()
    {
        go = true;
        sound.Play();
    }
}
