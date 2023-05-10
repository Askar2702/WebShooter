using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Image _uiSignalDamage;
    [SerializeField] private TextMeshProUGUI _healthBar;
    [SerializeField] private Slider _sliderHealt;
    [SerializeField] private Image _fill;
    [SerializeField] private Color _health100;
    [SerializeField] private Color _health50;
    [SerializeField] private Color _health25;
    public bool IsAlive { get; private set; }
    private void Awake()
    {
     //   _healthBar.text = _health.ToString();
        IsAlive = true;
        _sliderHealt.maxValue = _health;
        _sliderHealt.value = _health;
        if (_sliderHealt.value == _sliderHealt.maxValue)
            _fill.color = _health100;
    }

   
    public void TakeDamage(float amount , UnityEvent eventHealth)
    {
        _health -= amount;
        if (_health <= 0)
        {
            _health = 0;
            IsAlive = false;
            eventHealth?.Invoke();
        }
      //  _healthBar.text = _health.ToString();
        _sliderHealt.value = _health;
        if (_sliderHealt.value <= (_sliderHealt.maxValue / 2) && _sliderHealt.value > (_sliderHealt.maxValue / 3))
            _fill.color = _health50;
        if (_sliderHealt.value <= (_sliderHealt.maxValue / 3))
            _fill.color = _health25;

        _uiSignalDamage.color = new Color(255, 0, 0, 150);
        StartCoroutine(RestartSignal());
    }

    IEnumerator RestartSignal()
    {
        yield return new WaitForSeconds(0.5f);
        _uiSignalDamage.color = new Color(255, 0, 0, 0);
    }
}
