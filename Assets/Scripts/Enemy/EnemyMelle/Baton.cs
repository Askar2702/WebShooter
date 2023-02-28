using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baton : MonoBehaviour
{
    [SerializeField] private float _damage;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
        }
    }

}
