using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterMovementScript : MonoBehaviour
{
    private NavMeshAgent _agent;
    private PlayerMovementScript _player;
    private GameObject _target;
    private bool _isAlive = true;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerMovementScript>();
        _target = _player.gameObject;
    }

    void Update()
    {
        if (_isAlive == true)
        {
            _agent.destination = _target.transform.position;
        }
        else
        {
            _agent.destination = transform.position;
        }
    }

    /// <summary>
    /// Остановка движения
    /// </summary>
    /// <param name="isDeath"></param>
    public void Stopping(bool isDeath)
    {
        _isAlive = isDeath;
    }
}
