using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderUp : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpForce);
            Debug.Log("Yes");
        }
    }
}
