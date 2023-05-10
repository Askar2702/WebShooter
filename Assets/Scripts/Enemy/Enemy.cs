
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent MeshAgent { get; private set; }
    private Rigidbody[] _rbs;
    [SerializeField] private Animator _animator;
    private Health _health;
    [HideInInspector]
    public bool isDamage;
    [SerializeField] private ShieldDroneEnemy Drone;
    [SerializeField] private AudioSource _audioSource;
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
        GameManager.instance.OffEnemySound.AddListener(() => { 
            if(_audioSource!=null)
                _audioSource.volume = 0; 
        });
        GameManager.instance.OnEnemySound.AddListener(() => { 
            if(_audioSource!=null)
                _audioSource.volume = 1; 
        });
    }


   
    private void DisableRb()
    {
        foreach (var rb in _rbs)
        {
            rb.isKinematic = true;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }
    }

    public void TakeDamage(float damage , bool isHeadShot)
    {
        if (!IsAlive()) return;
       
        isDamage = true;
        _health.TakeDamege(damage);
        if (!IsAlive())
        {
            MeshAgent.enabled = false;
            if (!Drone)
                _animator.enabled = false;
            foreach (var rb in _rbs) rb.isKinematic = false;

            ScoreManager.instance.AddScore(isHeadShot);
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
