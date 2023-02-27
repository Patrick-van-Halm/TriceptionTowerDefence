using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private AudioReference _moveSound;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        UpdateIsMovingProperty();
    }

    private void UpdateIsMovingProperty()
    {
        _animator.SetBool("IsMoving", _navMeshAgent.velocity.magnitude > .1f);
    }

    public void PlaySound()
    {
        AudioManager.Instance?.PlaySound(gameObject, _moveSound);
    }
}
