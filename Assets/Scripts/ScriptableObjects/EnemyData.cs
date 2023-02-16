using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Triception TD/Enemy")]
public class EnemyData : ScriptableObject
{
    public int Health => _health;
    [SerializeField] private int _health;

    public int Money => _money;
    [SerializeField] private int _money;

    public float Speed => _speed;
    [SerializeField] private float _speed;
}
