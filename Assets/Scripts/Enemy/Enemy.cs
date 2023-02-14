using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _meshAgent;
    private Vector3 _targetPos;
    private Rigidbody[] _rbs;
    [SerializeField] private Animator _animator;
    private Health _health;
    void Start()
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        _targetPos = MazeSpawner.instance.GetRandomPos();
        _rbs = GetComponentsInChildren<Rigidbody>();
        _health = GetComponent<Health>();
        DisableRb();
    }


    void Update()
    {
        if (_meshAgent.enabled)
        {
            _meshAgent.SetDestination(_targetPos);
            if (Vector3.Distance(transform.position, _targetPos) < 3f) _targetPos = MazeSpawner.instance.GetRandomPos();
        }
    }
    private void DisableRb()
    {
        foreach (var rb in _rbs)
        {
            rb.isKinematic = true;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamege(damage);
        if (!_health.isAlive)
        {
            _meshAgent.enabled = false;
            _animator.enabled = false;
            foreach (var rb in _rbs) rb.isKinematic = false;

            Destroy(gameObject, 3f);
        }
    }

    public float GetAmountDamageDealt()
    {
        return _health.GetAmountDamageDealt();
    }
}
