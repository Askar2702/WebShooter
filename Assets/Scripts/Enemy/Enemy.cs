
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
    [SerializeField] private RifleEnemAnimation _enemyAnim;
    private NavMeshPath _path;
    private PlayerInput _player;
    private bool isPlayerTarget;

    [SerializeField] private LayerMask _mask;

    private enum DirectionMove { Forward , Back , Right , Left}
    private DirectionMove _directionMove = DirectionMove.Forward;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerInput>();
    }
    void Start()
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        _targetPos = PointsManager.instance.GetRandomPos();
        _rbs = GetComponentsInChildren<Rigidbody>();
        _health = GetComponent<Health>();
        DisableRb();
    }


    void Update()
    {
        if (_meshAgent.enabled)
        {
            _meshAgent.SetDestination(_targetPos);
            if (Vector3.Distance(transform.position, _targetPos) < 0.5f && !isPlayerTarget)
            {
                _targetPos = PointsManager.instance.GetRandomPos();
                _enemyAnim.ShowIdle();
            }
            else if (Vector3.Distance(transform.position, _targetPos) >= 0.5f) _enemyAnim.ShowWalkForward();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _meshAgent.Move(-transform.forward * Time.deltaTime);
        }
       
        PlayerAnalysis();
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
        if (!IsAlive())
        {
            _meshAgent.enabled = false;
            _animator.enabled = false;
            foreach (var rb in _rbs) rb.isKinematic = false;

            Destroy(gameObject, 3f);
        }
    }

    public bool IsAlive()
    {
        return _health.isAlive;
    }

    public float GetAmountDamageDealt()
    {
        return _health.GetAmountDamageDealt();
    }

   

    private void PlayerAnalysis()
    {
        if (DistancePlayer() < 10 && DistancePlayer() > 7)
        {
            transform.LookAt(_player.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
           // _meshAgent.velocity = Vector3.zero;

            // _enemyAnim.ShowIdle();
            RigthOrLeftMove();
            isPlayerTarget = true;
           
        }
        else if (DistancePlayer() < 7)
        {
            transform.LookAt(_player.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            var backPos = -transform.forward * Time.fixedDeltaTime * 50f;
          
            if (MyCollisions(-transform.forward))
            {
                _meshAgent.velocity = backPos;
                _enemyAnim.ShowWalkBack();
            }
            else
            {
                _enemyAnim.ShowIdle();
                _meshAgent.velocity = Vector3.zero;
            }
        }
        else isPlayerTarget = false;
    }

    private void RigthOrLeftMove()
    {
        if (MyCollisions(transform.right) && _directionMove!=DirectionMove.Left)
        {
            var rightPos = transform.right * Time.fixedDeltaTime * 50f;
            _enemyAnim.ShowWalkRight();
            _meshAgent.velocity = rightPos;
            _directionMove = DirectionMove.Right;
            print("right");
            return;
        }
        else if (MyCollisions(-transform.right) && _directionMove != DirectionMove.Right)
        {
            var leftPos = -transform.right * Time.fixedDeltaTime * 50f;
            _enemyAnim.ShowWalkLeft();
            _meshAgent.velocity = leftPos;
            _directionMove = DirectionMove.Left;
            print("left");
            return;
        }
        else  if (!MyCollisions(-transform.right) && !MyCollisions(transform.right))
        {
            _enemyAnim.ShowIdle();
            _directionMove = DirectionMove.Forward;
            _meshAgent.velocity = Vector3.zero;
            print("idles");
        }
        else
        {
            _directionMove = DirectionMove.Forward;
            _meshAgent.velocity = Vector3.zero;
        }
    }

    private float DistancePlayer()
    {
        return Vector3.Distance(_player.transform.position, transform.position);
    }
    private bool MyCollisions(Vector3 direction)
    {
        var pos = transform.position + direction + transform.up;
        Collider[] hitColliders = Physics.OverlapBox(pos, transform.localScale / 2, Quaternion.identity, _mask);
        bool isDetect = hitColliders.Length > 0 ? false : true;
        return isDetect;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var pos = transform.position - transform.forward + transform.up;
        Gizmos.DrawWireCube(pos, transform.localScale);

        Gizmos.color = Color.red;
        var pos1 = transform.position - transform.right + transform.up;
        Gizmos.DrawWireCube(pos1, transform.localScale);

        Gizmos.color = Color.red;
        var pos2 = transform.position + transform.right + transform.up;
        Gizmos.DrawWireCube(pos2, transform.localScale);
    }
}
