using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    public GameObject[] Enemies;
    public Transform EnemySpawn;
    public Transform[] PathWaypoints;
    private Dictionary<NavMeshAgent, int> navMeshWaypoints = new();

    IEnumerator Start()
    {
        var i = 0;
        while (i != 10)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(.6f);
            i++;    
        }
    }

    private void Update()
    {
        foreach(NavMeshAgent agent in navMeshWaypoints.Keys.ToList())
        {
            if (agent.remainingDistance > .1f) continue;

            int currentDestId = navMeshWaypoints[agent];
            if (currentDestId + 1 == PathWaypoints.Length)
            {
                AgentReachedEnd(agent);
                continue;
            }

            navMeshWaypoints[agent] = currentDestId + 1;
            agent.SetDestination(PathWaypoints[currentDestId + 1].position);
        }
    }

    private void AgentReachedEnd(NavMeshAgent agent)
    {
        if (!navMeshWaypoints.ContainsKey(agent)) return;
        Destroy(agent.gameObject);
        navMeshWaypoints.Remove(agent);
        HealthManager.Instance.TakeDamage(3);
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(Enemies[UnityEngine.Random.Range(0, Enemies.Length)], EnemySpawn.position, EnemySpawn.rotation);
        var navMesh = enemy.GetComponent<NavMeshAgent>();

        navMesh.SetDestination(PathWaypoints[0].position);
        navMeshWaypoints.Add(navMesh, 0);
    }
}
