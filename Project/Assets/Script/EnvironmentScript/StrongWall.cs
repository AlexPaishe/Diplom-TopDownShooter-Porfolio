using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongWall : MonoBehaviour
{
    [SerializeField] private Material[] _matUP;
    [SerializeField] private Material[] _matDown;
    [SerializeField] private float[] _step;

    private float _offsetUp;
    private float _offsetDown;

    void Update()
    {
        _offsetUp -= _step[0];
        _offsetDown += _step[1];
        for(int i = 0; i < _matUP.Length; i++)
        {
            _matUP[i].mainTextureOffset = new Vector2(0, _offsetUp);
        }
        for (int i = 0; i < _matDown.Length; i++)
        {
            _matDown[i].mainTextureOffset = new Vector2(0, _offsetDown);
        }
    }
}
