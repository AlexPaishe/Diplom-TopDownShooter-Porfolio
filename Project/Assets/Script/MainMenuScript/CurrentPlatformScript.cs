using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlatformScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _platforms;

    private void Awake()
    {
        int rand = Random.Range(0, _platforms.Length);
        _platforms[rand].transform.position = transform.position;
    }
}
