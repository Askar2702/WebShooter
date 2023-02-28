using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Enemy _enemy;
    private float _reactionDistance = 20f;
    private float _attackDistance = 3f;
    private bool isPlayerTarget;
    private Transform _target;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        _target = MazeSpawner.instance.GetRandomPos();
    }

    void Update()
    {
        PlayerAnalysis();
    }

    private void PlayerAnalysis()
    {
        if (_enemy.MeshAgent.enabled)
        {
            if (DistancePlayer() < _reactionDistance)
            {
                if (_target != _enemy.Player.transform) _target = _enemy.Player.transform;
                isPlayerTarget = true;
                if (DistancePlayer() <= _attackDistance)
                    _animator.SetInteger("AnimState", 2);
                else _animator.SetInteger("AnimState", 1);

            }
            else
            {
                isPlayerTarget = false;
                if (_target == _enemy.Player.transform) _target = MazeSpawner.instance.GetRandomPos();
                if (Vector3.Distance(transform.position, _target.position) < 0.5f && !isPlayerTarget)
                {
                    _target = MazeSpawner.instance.GetRandomPos();
                    _animator.SetInteger("AnimState", 0);
                }
                else _animator.SetInteger("AnimState", 1);

            }

            _enemy.MeshAgent.SetDestination(_target.position);
        }
    }

    private float DistancePlayer()
    {
        return Vector3.Distance(_enemy.Player.transform.position, transform.position);
    }
}
