using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLegScript : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position - _player.position;
    }

    void Update()
    {
        transform.position = _player.position + offset;
    }
}
