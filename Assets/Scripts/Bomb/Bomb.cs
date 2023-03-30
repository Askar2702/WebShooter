using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _force;
    private float _forceReducer = 1f;
    private float _minForce = 10f;
    [SerializeField] private float _time;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private float _maxRadius;
    [SerializeField] private ParticleSystem _explosion;
   
    public IEnumerator Init(Vector3 dir)
    {
        _rb.AddForce(dir, ForceMode.Force);
        yield return new WaitForSeconds(_time);
        _sphereCollider.enabled = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Enemy enemy))
        {
            var dir = enemy.transform.position - transform.position;
            dir += Vector3.up;
            enemy.TakeDamage(_damage , false);
            if (other.GetComponent<Rigidbody>())
                other.GetComponent<Rigidbody>().AddForce(dir * _force, ForceMode.Impulse);
        }
    }


   
}
