using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRifle : MonoBehaviour
{
    [SerializeField] private BulletEnemy _bullet;
    [SerializeField] private Transform _spawnBulletPoint;
    [SerializeField] private ParticleSystem _muzzleFalshEffect;
    [SerializeField] private AudioSource _fireSound;
    [SerializeField] private float _interval;
    [SerializeField] private LayerMask _layerMask;
    private Enemy _enemy;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }
    private bool isReady;

    private void Start()
    {
        isReady = true;
        StartCoroutine(UpdateState());
    }
    IEnumerator UpdateState()
    {
        while (_enemy.IsAlive()) 
        {

            if (_enemy.isPlayerTarget && isReady)
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
                return;
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
}
