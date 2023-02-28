
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent MeshAgent { get; private set; }
    private Rigidbody[] _rbs;
    [SerializeField] private Animator _animator;
    private Health _health;
    public bool isDamage;

  
    public Player Player { get; private set; }
  
 

   

   
    private void Awake()
    {
        Player = FindObjectOfType<Player>();
    }
    void Start()
    {
        MeshAgent = GetComponent<NavMeshAgent>();
        isDamage = false;
        _rbs = GetComponentsInChildren<Rigidbody>();
        _health = GetComponent<Health>();
        DisableRb();
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
        isDamage = true;
        _health.TakeDamege(damage);
        if (!IsAlive())
        {
            MeshAgent.enabled = false;
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

   

  
}
