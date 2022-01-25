using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterMovementScript)),
    RequireComponent(typeof(MeshRenderer)),
    RequireComponent(typeof(EnemySoundScript))]
public class HealthScriptMonster : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _reward;

    [SerializeField] private GameObject _unit;

    private Animator _anima;
    private MeshRenderer _meshRender;
    private Vector3 _begin;
    private InterfaceScript _interfaces;
    private SpawnerScript _spawn;
    private MonsterMovementScript _monster;
    private PlayerMovementScript _player;
    private EnemySoundScript _sound;

    private bool _isAlive = true;
    private int _maxhealth;

    private void Start()
    {
        _spawn = FindObjectOfType<SpawnerScript>();
        _maxhealth = _currentHealth;
        _interfaces = FindObjectOfType<InterfaceScript>();
        _monster = GetComponent<MonsterMovementScript>();
        _anima = _unit.GetComponent<Animator>();
        _meshRender = _unit.GetComponent<MeshRenderer>();
        _player = FindObjectOfType<PlayerMovementScript>();
        _sound = GetComponent<EnemySoundScript>();
    }

    private void Update()
    {
        if(_player.gameObject.transform.position.y < transform.position.y - 12)
        {
            _currentHealth = _maxhealth;
            int rand = Random.Range(0, _spawn._enemiesSpawn.Length);
            _begin = _spawn._enemiesSpawn[rand];
            transform.position = _begin;
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Нанесение урона врагу
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (_isAlive == true)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _isAlive = false;
                StartCoroutine(Death());
            }
            if(_currentHealth > 0)
            {
                StartCoroutine(Damage());
            }
        }
    }

    /// <summary>
    /// Реализация смерти
    /// </summary>
    /// <returns></returns>
    IEnumerator Death()
    {
        _monster.Stopping(false);
        _interfaces.Score(_reward);
        gameObject.layer = 10;
        _anima.SetBool("Iddle", false);
        _sound.ConditionSound(1);
        for (int i = 0; i < _meshRender.materials.Length; i++)
        {
            _meshRender.materials[i].EnableKeyword("_EMISSION");
        }
        yield return new WaitForSeconds(0.8f);
        _meshRender.enabled = false;
        _anima.SetBool("Iddle", true);
        yield return new WaitForSeconds(0.2f);
        _isAlive = true;
        _currentHealth = _maxhealth;
        int rand = Random.Range(0, _spawn._enemiesSpawn.Length);
        _begin = _spawn._enemiesSpawn[rand];
        transform.position = _begin;
        gameObject.layer = 0;
        _meshRender.enabled = true;
        for (int i = 0; i < _meshRender.materials.Length; i++)
        {
            _meshRender.materials[i].DisableKeyword("_EMISSION");
        }
        _monster.Stopping(true);
        _sound.ConditionSound(0);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Реализация урона
    /// </summary>
    /// <returns></returns>
    IEnumerator Damage()
    {
        for (int i = 0; i < _meshRender.materials.Length; i++)
        {
            _meshRender.materials[i].EnableKeyword("_EMISSION");
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < _meshRender.materials.Length; i++)
        {
            _meshRender.materials[i].DisableKeyword("_EMISSION");
        }
    }
}
