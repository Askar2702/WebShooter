using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _health;
    [SerializeField] private Image _uiSignalDamage;
    [SerializeField] private TextMeshProUGUI am;
    [SerializeField] private RectTransform _sl;
    public bool IsAlive { get; private set; }
    private void Awake()
    {
        _slider.maxValue = _health;
        _slider.value = _health;
        IsAlive = true;
    }

    private void Update()
    {
        am.text = _sl.transform.position.ToString();
    }
    public void TakeDamage(float amount , UnityEvent eventHealth)
    {
        _health -= amount;
        am.text = _slider.value.ToString();
        if (_health <= 0)
        {
            _health = 0;
            IsAlive = false;
            eventHealth?.Invoke();
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
