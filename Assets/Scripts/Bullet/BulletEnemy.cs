using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;
    Vector3 _direction;
    public void Init(Vector3 dir)
    {
        _direction = dir;
        Destroy(gameObject, 10f);
    }
    private void FixedUpdate()
    {
        _rb.velocity = _direction * _speed;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
            Destroy(gameObject);
        }
        if (other.gameObject.layer == 6)
            Destroy(gameObject);
    }

    

}
