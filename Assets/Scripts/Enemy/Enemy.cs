using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData _data;
    [SerializeField, ReadOnly] private int _health;
    private int _waypointId;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _health = _data.Health;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _data.Speed;

        _waypointId = 0;
        _agent.SetDestination(WaveManager.Instance.GetWaypointAt(_waypointId));
    }

    private void Update()
    {
        if (_agent.remainingDistance < .5 && !WaveManager.Instance.IsLastWaypoint(_waypointId))
            SetNextPathingWaypoint();
        else if (_agent.remainingDistance < .5 && WaveManager.Instance.IsLastWaypoint(_waypointId))
            DamageTarget();
    }

    private void DamageTarget()
    {
        HealthManager.Instance.TakeDamage(_health);
        Destroy(gameObject);
    }

    private void SetNextPathingWaypoint()
    {
        _waypointId++;
        _agent.SetDestination(WaveManager.Instance.GetWaypointAt(_waypointId));
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Kill();
    }

    private void Kill()
    {
        MoneyManager.Instance.Gain(_data.Money);
        Destroy(gameObject);
    }
}
