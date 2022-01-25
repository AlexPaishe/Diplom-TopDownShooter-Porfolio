using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class CursorTrainingScript : MonoBehaviour
{
    private MeshRenderer _mesh;
    private CameraMainMenuScript _cam;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _cam = FindObjectOfType<CameraMainMenuScript>();
    }

    void Update()
    {
        if(_cam.ForwardBool() == true)
        {
            _mesh.enabled = true;
        }
        else
        {
            _mesh.enabled = false;
        }
    }
}
