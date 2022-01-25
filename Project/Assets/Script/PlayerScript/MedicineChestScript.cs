using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineChestScript : MonoBehaviour
{
    private Bonus _bonus;

    void Start()
    {
        _bonus = FindObjectOfType<Bonus>();
    }

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthPlayer>().Healing();
            _bonus.NewBonus();
            Destroy(gameObject);
        }
    }
}
