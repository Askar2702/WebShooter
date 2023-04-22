using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SkinnedMeshRenderer[] _skinneds;
    [Space(30)]

    [SerializeField] private ParticleSystem _startTeleport;
    private Enemy _enemy;
    private float _reactionDistance = 20f;
    private float _attackDistance = 3f;
    private bool isPlayerTarget;
    private Transform _target;
    private float _teleportDistance = 10f;
    [SerializeField] private AudioSource _audio;
    
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _skinneds[Random.Range(0, _skinneds.Length)].gameObject.SetActive(true);
    }

    private void Start()
    {
        _target = PointsManager.instance.GetRandomPos();
    }

    void Update()
    {
        PlayerAnalysis();
    }

    private void PlayerAnalysis()
    {
        if (_enemy.MeshAgent.enabled)
        {
            if (DistancePlayer() < _reactionDistance || _enemy.isDamage)
            {
                if (_target != _enemy.Player.transform) _target = _enemy.Player.transform;
                isPlayerTarget = true;
                if (DistancePlayer() <= _attackDistance)
                {
                    _animator.SetInteger("AnimState", 2);
                    if (!_audio.isPlaying)
                        _audio.Play();
                }
                else _animator.SetInteger("AnimState", 1);
                WrapEnemy();

            }
            else
            {
                isPlayerTarget = false;
                if (_target == _enemy.Player.transform) _target = PointsManager.instance.GetRandomPos();
                if (Vector3.Distance(transform.position, _target.position) < 1f && !isPlayerTarget)
                {
                    _target = PointsManager.instance.GetRandomPos();
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

    private void WrapEnemy()
    {
        _target = Player.instance.transform;
        float distanceToTarget = Vector3.Distance(transform.position, _target.position);

        if (distanceToTarget > _teleportDistance)
        {
            int segments = Mathf.CeilToInt(distanceToTarget / 5f);
            Vector3 lastSegmentPosition = transform.position;

            for (int i = 1; i <= segments; i++)
            {
                Vector3 segmentPosition = Vector3.Lerp(transform.position, _target.position, i / (float)segments);
                if (Vector3.Distance(segmentPosition, _target.position) < 2f)
                {
                    segmentPosition = _target.position;
                    break;
                }
                else if (i == segments)
                {
                    break;
                }

                Vector3 teleportPosition = GetTeleportPosition(segmentPosition);
               
                _enemy.MeshAgent.Warp(teleportPosition);
                lastSegmentPosition = teleportPosition;
            }
            _startTeleport.Play();
            _enemy.MeshAgent.SetDestination(lastSegmentPosition);
           
        }
        else
        {
            _enemy.MeshAgent.SetDestination(_target.position);
        }
    }


    private Vector3 GetTeleportPosition(Vector3 targetPosition)
    {
        NavMeshHit hit;
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        Vector3 randomPosition = targetPosition + randomDirection * 5;
        float distanceToPlayer = Vector3.Distance(_enemy.Player.transform.position, randomPosition);

        // генерируем новую случайную позицию, пока расстояние до цели не будет больше, чем 2 метра
        while (Vector3.Distance(randomPosition, targetPosition) < 3f || distanceToPlayer < 2f)
        {
            randomDirection = Random.insideUnitSphere.normalized;
            randomPosition = targetPosition + randomDirection * 5;
            distanceToPlayer = Vector3.Distance(_enemy.Player.transform.position, randomPosition);
        }
       
        // ищем ближайшую доступную точку на навигационной сетке для новой позиции
        if (NavMesh.SamplePosition(randomPosition, out hit, 3f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        

        // если не удалось найти доступную точку, возвращаем исходную позицию
        return transform.position;
    }


}
