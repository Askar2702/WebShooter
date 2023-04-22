using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShieldDroneEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _reactDistance;
    [SerializeField] private float _reactDistancePlayer;
    [field:SerializeField] public Shield Shield { get; private set; }
    private Vector3 _targetPos;
    private bool isProtected;
  [field: SerializeField]  public EnemyRifle EnemyRifle { get; private set; }
    void Start()
    {
        _targetPos = PointsManager.instance.GetRandomPos().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy.MeshAgent.enabled)
        {
            _enemy.MeshAgent.SetDestination(_targetPos);
            if (Vector3.Distance(transform.position, _targetPos) < 2.5f && !isProtected)
            {
                _targetPos = PointsManager.instance.GetRandomPos().position;
                print("walk");
            }

            if (DistancePlayer() <= _reactDistancePlayer)
            {
                transform.LookAt(_enemy.Player.transform);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

                if (FindClosestRifle() && FindClosestRifle().isPlayerTarget
               && Vector3.Distance(FindClosestRifle().transform.position, transform.position) <= _reactDistance)
                {
                    EnemyRifle = FindClosestRifle();
                    _targetPos = EnemyRifle.transform.position + (EnemyRifle.transform.forward * 5);
                    Debug.DrawRay(transform.position, FindClosestRifle().transform.position, Color.green);
                    isProtected = true;
                    print("protected");
                }
            }

            if (FindClosestRifle() && Vector3.Distance(FindClosestRifle().transform.position, transform.position) > _reactDistance)
            {
                if (isProtected)
                {
                    isProtected = false;
                    EnemyRifle = null;
                    _targetPos = PointsManager.instance.GetRandomPos().position;
                    print("Notprotected");
                }
            }

           

        }

    }

    private float DistancePlayer()
    {
        return Vector3.Distance(_enemy.Player.transform.position, transform.position);
    }

    private EnemyRifle FindClosestRifle()
    {
        EnemyRifle enemyRifle = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (var e in EnemySpawn.instance.GettingEnemyRifles())
        {
            bool isEnemy = true;
            foreach(var shield in EnemySpawn.instance.GetShieldDroneEnemies())
            {
                if(shield !=this && shield.EnemyRifle == e)
                {
                    isEnemy = false;
                    break;
                }
            }
            float dist = Vector3.Distance(e.transform.position, currentPos);
            if (dist < minDist && isEnemy)
            {
                enemyRifle = e;
                minDist = dist;
            }
        }
        return enemyRifle;
    }

    private void OnDestroy()
    {
        EnemySpawn.instance.DeleteShield(this);
    }
}
