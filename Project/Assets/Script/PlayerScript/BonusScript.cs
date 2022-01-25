using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _bonusVariation;
    private int _bonusNumber;

    private Bonus _bonus;

    void Start()
    {
        _bonus = FindObjectOfType<Bonus>();
        _bonusNumber = Random.Range(0, _bonusVariation.Length);
        for(int i = 0; i < _bonusVariation.Length;i++)
        {
            if(i == _bonusNumber)
            {
                _bonusVariation[i].SetActive(true);
            }
            else
            {
                _bonusVariation[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ShootingScript>().NewWeapon(_bonusNumber);
            _bonus.NewBonus();
            Destroy(gameObject);
        }
    }
}
