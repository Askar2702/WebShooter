using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRifle : MonoBehaviour
{
    #region MoveEnemy

    private Vector3 _targetPos;
    private float _reactionDistance = 20f;
    private float _minDistance = 10;
    private enum DirectionMove { Forward, Back, Right, Left }
    private DirectionMove _directionMove = DirectionMove.Forward;
    [SerializeField] private LayerMask _mask;
    #endregion

    #region FireWeapon
    [SerializeField] private BulletEnemy _bullet;
    [SerializeField] private Transform _spawnBulletPoint;
    [SerializeField] private ParticleSystem _muzzleFalshEffect;
    [SerializeField] private AudioSource _fireSound;
    [SerializeField] private float _interval;
    [SerializeField] private LayerMask _layerMask;
    public bool isPlayerTarget { get; private set; }
    #endregion
    private Enemy _enemy;
    private RifleEnemAnimation EnemyAnim;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        EnemyAnim = GetComponent<RifleEnemAnimation>();
    }
    private bool isReady;

    private void Start()
    {
        isReady = true;
        _targetPos = MazeSpawner.instance.GetRandomPos().position;
        StartCoroutine(UpdateState());
    }

    #region Move
    void Update()
    {
        if (_enemy.MeshAgent.enabled)
        {
            _enemy.MeshAgent.SetDestination(_targetPos);
            if (Vector3.Distance(transform.position, _targetPos) < 0.5f && !isPlayerTarget)
            {
                _targetPos = MazeSpawner.instance.GetRandomPos().position;
                EnemyAnim.ShowIdle();
            }
            else if (Vector3.Distance(transform.position, _targetPos) >= 0.5f) EnemyAnim.ShowWalkForward();
            PlayerAnalysis();
        }
    }

    private void PlayerAnalysis()
    {
        if (DistancePlayer() < _reactionDistance && DistancePlayer() > _minDistance || _enemy.isDamage)
        {
            transform.LookAt(_enemy.Player.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
           
            RigthOrLeftMove();
            isPlayerTarget = true;

        }
        else if (DistancePlayer() < _minDistance)
        {
            transform.LookAt(_enemy.Player.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            var backPos = -transform.forward * Time.fixedDeltaTime * 50f;

            if (MyCollisions(-transform.forward))
            {
                _enemy.MeshAgent.velocity = backPos;
                EnemyAnim.ShowWalkBack();
            }
            else
            {
                RigthOrLeftMove();
            }
        }
        else isPlayerTarget = false;
    }

    private void RigthOrLeftMove()
    {
        if (MyCollisions(transform.right) && _directionMove != DirectionMove.Left)
        {
            var rightPos = transform.right * Time.fixedDeltaTime * 50f;
            EnemyAnim.ShowWalkRight();
            _enemy.MeshAgent.velocity = rightPos;
            _directionMove = DirectionMove.Right;
            return;
        }
        else if (MyCollisions(-transform.right) && _directionMove != DirectionMove.Right)
        {
            var leftPos = -transform.right * Time.fixedDeltaTime * 50f;
            EnemyAnim.ShowWalkLeft();
            _enemy.MeshAgent.velocity = leftPos;
            _directionMove = DirectionMove.Left;
            return;
        }
        else if (!MyCollisions(-transform.right) && !MyCollisions(transform.right))
        {
            EnemyAnim.ShowIdle();
            _directionMove = DirectionMove.Forward;
            _enemy.MeshAgent.velocity = Vector3.zero;
        }
        else
        {
            _directionMove = DirectionMove.Forward;
            _enemy.MeshAgent.velocity = Vector3.zero;
        }
    }

    private float DistancePlayer()
    {
        return Vector3.Distance(_enemy.Player.transform.position, transform.position);
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
    #endregion


    #region Fire
    IEnumerator UpdateState()
    {
        while (_enemy.IsAlive()) 
        {

            if (isPlayerTarget && isReady)
            {
                Fire();
            }
            yield return null;
        }
    }
    private void Fire()
    {
        _spawnBulletPoint.LookAt(_enemy.Player.transform);
        RaycastHit hit;
        if (Physics.Raycast(_spawnBulletPoint.position, _spawnBulletPoint.forward, out hit, Mathf.Infinity, _layerMask))
        {
            if (hit.transform.gameObject.layer != 8)
            {
                _enemy.isDamage = false;
                return;
            }
        }
        
        var bullet =  Instantiate(_bullet, _spawnBulletPoint.position, _spawnBulletPoint.rotation);
        bullet.Init(_spawnBulletPoint.forward);

        _muzzleFalshEffect.Play();

        _fireSound.Play();
        isReady = false;

        StartCoroutine(ResetReady());
    }
    
    IEnumerator ResetReady()
    {
        yield return new WaitForSeconds(_interval);
        isReady = true;
    }
    #endregion

    private void OnDisable()
    {
        EnemySpawn.instance.DeletelSelf(this);
    }
}
