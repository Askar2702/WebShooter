using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] protected Health _baseHealth;
    [SerializeField] private float _healthCount;
    [SerializeField] private Animation _animation;
    [SerializeField] private Material _shielMat;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Collider _collider;
    [ColorUsage(true, true)]
    [SerializeField] private Color _colorShield;
    private float _currentHealthCount;
    public bool isAlive { get; private set; }

    private void Awake()
    {
        Rise();
    }

    public void TakeDamege(float amount)
    {
        if (!isAlive) return;
        _currentHealthCount -= amount;
        if(_currentHealthCount <= _healthCount / 1.5 && !_animation.isPlaying)
        {
            _animation.Play();
        }
        if (_currentHealthCount <= 0)
        {
            _currentHealthCount = 0;
            isAlive = false;
            StartCoroutine(RestartShield());
        }
    }

    private IEnumerator RestartShield()
    {
        _mesh.enabled = false;
        _collider.enabled = false;
        _animation.Stop();
        yield return new WaitForSeconds(3f);
        Rise();
        
    }

  
    private void Rise()
    {
        if (!_baseHealth.isAlive) return;
        isAlive = true;
        _currentHealthCount = _healthCount;
        _shielMat.color = _colorShield;
        _mesh.enabled = true;
        _collider.enabled = true;
    }
}
