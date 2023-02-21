using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _health;
    [SerializeField] private Image _uiSignalDamage;
    private void Awake()
    {
        _slider.maxValue = _health;
        _slider.value = _health;
    }
  

    public void TakeDamage(float amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            _health = 0;
        }
        _slider.value = _health;
        _uiSignalDamage.color = new Color(255, 0, 0, 150);
        StartCoroutine(RestartSignal());
    }

    IEnumerator RestartSignal()
    {
        yield return new WaitForSeconds(0.5f);
        _uiSignalDamage.color = new Color(255, 0, 0, 0);
    }
}
