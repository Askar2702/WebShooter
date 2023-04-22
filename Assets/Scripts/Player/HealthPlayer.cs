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
    public bool IsAlive { get; private set; }
    private void Awake()
    {
        _healthBar.text = _health.ToString();
        IsAlive = true;
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
        _healthBar.text = _health.ToString();

        _uiSignalDamage.color = new Color(255, 0, 0, 150);
        StartCoroutine(RestartSignal());
    }

    IEnumerator RestartSignal()
    {
        yield return new WaitForSeconds(0.5f);
        _uiSignalDamage.color = new Color(255, 0, 0, 0);
    }
}
