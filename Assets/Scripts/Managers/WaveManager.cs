using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : SingletonMonoBehaviour<WaveManager>
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Transform _enemySpawn;
    [SerializeField] private Transform[] _pathWaypoints;

    private IEnumerator Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10 * UnityEngine.Random.Range(1.0f, i); j++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(.7f);
            }
            yield return new WaitForSeconds(10);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemies[UnityEngine.Random.Range(0, _enemies.Length)], _enemySpawn.position, _enemySpawn.rotation);
    }

    public bool IsLastWaypoint(int waypointId)
    {
        return waypointId == _pathWaypoints.Length - 1;
    }

    public Vector3 GetWaypointAt(int waypointId)
    {
        return _pathWaypoints[waypointId].position;
    }
}
