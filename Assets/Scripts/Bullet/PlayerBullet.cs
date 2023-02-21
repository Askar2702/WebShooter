using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;
    Vector3 _direction;
    public void Init(Vector3 dir)
    {
        _direction = dir;
        Destroy(gameObject, 1f);
    }
    private void FixedUpdate()
    {
       // _rb.velocity = transform.forward * _speed;
        _rb.velocity = _direction * _speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 9)
            Destroy(gameObject, 1f);
    }

    
    }

   


