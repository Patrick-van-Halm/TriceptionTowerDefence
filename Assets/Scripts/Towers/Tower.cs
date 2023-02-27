using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected TowerData _data;
    [SerializeField] protected Transform _barrelEnd;

    private Coroutine _towerLoop;
    private GameObject _rangeVisualizer;

    public abstract GameObject FindEnemy();
    public abstract void ShootEnemy(GameObject enemy);

    public void StartTowerLoop()
    {
        if (_towerLoop != null) return;
        _towerLoop = StartCoroutine(TowerLoop());
    }

    private IEnumerator TowerLoop()
    {
        while (true)
        {
            var enemy = FindEnemy();
            if (enemy)
            {
                ShootEnemy(enemy);
                Instantiate(_data.ProjectilePrefab, _barrelEnd.position, Quaternion.LookRotation(enemy.transform.position - transform.position));
                AudioManager.Instance.PlaySound(gameObject, _data.ShootAudio);
                yield return new WaitForSeconds(1 / (_data.RPM / 60));
            }
            else yield return null;
        }
    }

    public void EnableRangeVisualizer(bool enabled)
    {
        if (enabled && !_rangeVisualizer)
        {
            _rangeVisualizer = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            DestroyImmediate(_rangeVisualizer.GetComponent<Collider>());
            _rangeVisualizer.GetComponent<Renderer>().material = BuildModeManager.Instance.TowerRangeMaterial;
            _rangeVisualizer.transform.parent = transform;
            _rangeVisualizer.transform.localPosition = Vector3.zero;
            _rangeVisualizer.transform.localScale = new Vector3(_data.Range, .1f, _data.Range);
        }
        else if (!enabled)
        {
            Destroy(_rangeVisualizer);
        }
    }
}