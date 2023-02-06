using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _healthCount;
    public bool isAlive { get; private set; }

    private void Awake()
    {
        _slider.maxValue = _healthCount;
        _slider.value = _healthCount;
        isAlive = true;
    }

    public void TakeDamege(float amount)
    {
        _healthCount -= amount;
        if (_healthCount <= 0)
        {
            _healthCount = 0;
            isAlive = false;
        }
        _slider.value = _healthCount;
    }

    public float GetAmountDamageDealt()
    {
        return _slider.maxValue - _healthCount;
    }
}
