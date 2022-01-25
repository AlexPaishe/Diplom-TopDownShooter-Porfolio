using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private GameObject _cursor;
    [SerializeField] private float _maxDistance = 40f;

    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), _maxDistance, _layer);
        for(int i = 0; i< hits.Length; i++)
        {
            if(hits[i].transform.gameObject.layer == 8)
            {
                transform.position = hits[i].point;
                break;
            }
        }
    }

    /// <summary>
    /// Изменение курсора под вид оружия
    /// </summary>
    /// <param name="weapon"></param>
    public void CursorWeapon(int weapon)
    {
        switch(weapon)
        { 
            case 0:
                _cursor.GetComponent<MeshRenderer>().materials[0].color = Color.blue;
                break;
            case 1:
                _cursor.GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
                break;
            case 2:
                _cursor.GetComponent<MeshRenderer>().materials[0].color = Color.green;
                break;
            case 3:
                _cursor.GetComponent<MeshRenderer>().materials[0].color = Color.red;
                break;
        }
    }
}
