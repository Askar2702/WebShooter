using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;
  
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    private void FixedUpdate()
    {
        _rb.velocity = transform.forward * _speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 9)
            Destroy(gameObject, 1f);
    }

    
    }

   


