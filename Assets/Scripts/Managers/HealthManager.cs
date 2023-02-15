using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : SingletonMonoBehaviour<HealthManager>
{
    [SerializeField] private IntEvent _onHealthChanged;
    [SerializeField] private int _maxHealth;
    [SerializeField, ReadOnly] private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health < 0) _health = 0;
        _onHealthChanged?.Invoke(_health);
    }
}
