using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Scriptable Objects/Triception TD/Tower")]
public class TowerData : ScriptableObject
{
    public int Price => _price;
    [SerializeField] private int _price;

    public string DisplayName => _displayName;
    [SerializeField] private string _displayName;

    public string Description => _description;
    [SerializeField, TextArea] private string _description;

    public GameObject Prefab => _prefab;
    [SerializeField] private GameObject _prefab;

    public float Range => _range;
    [SerializeField] private float _range;

    public float RPM => _rpm;
    [SerializeField] private float _rpm;

    public int Damage => _damage;
    [SerializeField] private int _damage;

    public AudioReference ShootAudio => _shootAudio;
    [SerializeField] private AudioReference _shootAudio;

    public GameObject ProjectilePrefab => _projectilePrefab;
    [SerializeField] private GameObject _projectilePrefab;
}
