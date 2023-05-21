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
    [SerializeField] private Slider _sliderHealt;
    [SerializeField] private Image _fill;
    [SerializeField] private Color _health100;
    [SerializeField] private Color _health50;
    [SerializeField] private Color _health25;
    private bool _isDamage;

    public bool IsAlive { get; private set; }
    private void Awake()
    {
        IsAlive = true;
        _sliderHealt.maxValue = _health;
        _sliderHealt.value = _health;
       // StartCoroutine(UpdateHealth());
    }

    private void Update()
    {
        if (!_isDamage)
        {
            _health += Time.deltaTime * 10;
            _sliderHealt.value = _health;
            SetColorHealthBr();
        }
    }
    public void TakeDamage(float amount , UnityEvent eventHealth)
    {
        _isDamage = true;
        _health -= amount;
        _sliderHealt.value = _health;
        StartCoroutine(CheckDamage(_health));
        if (_health <= 0)
        {
            _health = 0;
            IsAlive = false;
            eventHealth?.Invoke();
        }
        SetColorHealthBr();

        _uiSignalDamage.color = new Color(255, 0, 0, 150);
        StartCoroutine(RestartSignal());
    }

    IEnumerator RestartSignal()
    {
        yield return new WaitForSeconds(0.5f);
        _uiSignalDamage.color = new Color(255, 0, 0, 0);
    }

    IEnumerator CheckDamage(float amount)
    {
        yield return new WaitForSeconds(2);
        if (amount == _health) _isDamage = false;
        else _isDamage = true;

    }

    IEnumerator UpdateHealth()
    {
        while (true)
        {
            //if (!_isDamage)
            //{
            //    _health += Time.deltaTime * 10;
            //    _sliderHealt.value = _health;
            //    SetColorHealthBr();
            //}
            yield return null;
        }
    }

    private void SetColorHealthBr()
    {
        if (_sliderHealt.value > (_sliderHealt.maxValue / 2))
            _fill.color = _health100;
        if (_sliderHealt.value <= (_sliderHealt.maxValue / 2) && _sliderHealt.value > (_sliderHealt.maxValue / 3))
            _fill.color = _health50;
        if (_sliderHealt.value <= (_sliderHealt.maxValue / 3))
            _fill.color = _health25;
    }
}
