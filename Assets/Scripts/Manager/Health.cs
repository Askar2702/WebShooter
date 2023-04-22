using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float _healthCount;
    public float _currentHealthCount;
    [SerializeField] private GameObject _miniMap;
    public bool isAlive { get; private set; }

    private void Awake()
    {
        isAlive = true;
        _currentHealthCount = _healthCount;
    }

    public void TakeDamege(float amount)
    {
        if (!isAlive) return;
        _currentHealthCount -= amount;
        if (_currentHealthCount <= 0)
        {
            _currentHealthCount = 0;
            isAlive = false;
            _miniMap.SetActive(false);
        }
    }

    public float GetAmountDamageDealt()
    {
        return _healthCount - _currentHealthCount;
    }
}
