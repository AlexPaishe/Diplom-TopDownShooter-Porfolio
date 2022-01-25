using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemy;
    [SerializeField] private Text _enemyText;
    private int monster;

    void Start()
    {
        monster = _enemy.Length;
        _enemyText.text = $"{monster}";
    }

    /// <summary>
    /// Реализация смерти и респауна
    /// </summary>
    /// <returns></returns>
    public bool Death()
    {
        monster--;
        bool death = true;
        if(monster > 0)
        {
            _enemyText.text = $"{monster}";
            death = false;
        }
        else if(monster == 0)
        {
            monster = _enemy.Length;
            _enemyText.text = $"{monster}";
            for(int i = 0; i < _enemy.Length; i ++)
            {
                _enemy[i].SetActive(true);
            }
            death = true;
        }
        return death;
    }
}
