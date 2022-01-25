using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrainingScript : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject Unit;

    private MeshRenderer meshRender;
    private TrainingSpawnScript spawn;

    private bool isAlive = true;
    private int maxhealth;

    private void Awake()
    {
        spawn = FindObjectOfType<TrainingSpawnScript>();
        meshRender = Unit.GetComponent<MeshRenderer>();
        maxhealth = currentHealth;
    }

    /// <summary>
    /// Нанесение урона врагу
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (isAlive == true)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                isAlive = false;
                StartCoroutine(Death());
            }
            if (currentHealth > 0)
            {
                StartCoroutine(Damage());
            }
        }
    }

    /// <summary>
    /// Реализация урона
    /// </summary>
    /// <returns></returns>
    IEnumerator Damage()
    {
        for (int i = 0; i < meshRender.materials.Length; i++)
        {
            meshRender.materials[i].EnableKeyword("_EMISSION");
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < meshRender.materials.Length; i++)
        {
            meshRender.materials[i].DisableKeyword("_EMISSION");
        }
    }

    /// <summary>
    /// Реализация смерти
    /// </summary>
    /// <returns></returns>
    IEnumerator Death()
    {
        gameObject.layer = 10;
        for (int i = 0; i < meshRender.materials.Length; i++)
        {
            meshRender.materials[i].EnableKeyword("_EMISSION");
        }
        yield return new WaitForSeconds(0.8f);
        isAlive = true;
        currentHealth = maxhealth;
        gameObject.layer = 0;
        for (int i = 0; i < meshRender.materials.Length; i++)
        {
            meshRender.materials[i].DisableKeyword("_EMISSION");
        }
        bool death = spawn.Death();
        if (death == false)
        {
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(newLive());
        }
    }

    private void OnEnable()
    {
        StartCoroutine(newLive());
    }

    /// <summary>
    /// воскрешение с огоньком
    /// </summary>
    /// <returns></returns>
    IEnumerator newLive()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < meshRender.materials.Length; i++)
        {
            meshRender.materials[i].DisableKeyword("_EMISSION");
        }
    }
}
