using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _force;
    [SerializeField] private float _chanceHeadShot;
    [SerializeField] private float _damage;
    [SerializeField] private Rigidbody _rb;
    private float _damageMultiplier = 100000f;
    private Vector3 _direction;
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }
    public void Init(Vector3 forceDirection)
    {
        _direction = forceDirection;
        Destroy(gameObject, 1f);
    }
    private void FixedUpdate()
    {
        _rb.velocity = transform.forward * _speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Enemy enemy)) 
        { 
          //  ShootEnemy(enemy , other.GetComponent<Rigidbody>());
          //  print(other.name);
        }
        if (other.gameObject.layer == 6 || other.gameObject.layer == 9)
            Destroy(gameObject, 1f);
    }

    private void ShootEnemy(Enemy e , Rigidbody rb)
    {
        var enemy = e;

        //if (_chanceHeadShot > Random.Range(0, 100))
        //{
        //    enemy.TakeDamage(_damage * _damageMultiplier ,rb.transform);
        //    enemy.GetHeadBody().AddForce(_direction * _force, ForceMode.Impulse);
        //    print("headShot");
        //}
       // else
        {
            enemy.TakeDamage(_damage , rb.transform);
            print("popal");
           // enemy.Rbs[Random.Range(0, enemy.Rbs.Length)].AddForce(dir * _force, ForceMode.Impulse);
           ///тут бью даже если у него есть хп , так если есть хп тот rb будет игнорировать вызов 
            rb.AddForce(transform.forward * _force, ForceMode.Impulse);
        }
    }

   

}
