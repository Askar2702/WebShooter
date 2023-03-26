using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _damage;
    [SerializeField] private float _force;
    private float _forceReducer = 1f;
    private float _minForce = 10f;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _maxRadius;
    [SerializeField] private ParticleSystem _explosion;

   

  


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    internal void Init(Vector3 forward)
    {
        _rb.AddForce(forward, ForceMode.Impulse);
        Destroy(gameObject, 5f);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6 ||
            collision.gameObject.layer == 9
            || collision.gameObject.layer == 12
            || collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(Explosion());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Enemy enemy))
        {
            var dir = enemy.transform.position - transform.position;
            dir += Vector3.up;
            enemy.TakeDamage(_damage);
            if (other.GetComponent<Rigidbody>())
                other.GetComponent<Rigidbody>().AddForce(dir * _force, ForceMode.Impulse);
        }
    }

    IEnumerator Explosion()
    {
        _sphereCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        Instantiate(_explosion, transform.position, Quaternion.identity);
        while (_sphereCollider.radius < _maxRadius)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _sphereCollider.radius += Time.deltaTime * 100f;
            _force -= _forceReducer;
            _force = _force <= 0 ? _minForce : _force;
            yield return null;
        }

        Destroy(gameObject);
    }
}
