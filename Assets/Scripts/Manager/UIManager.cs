using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private Image _aimSprite;
    [SerializeField] private TextMeshProUGUI _uiAmountDamage;
    [SerializeField] private Transform _pointSpawnUIDamage;
    private float _posSpawnRange = 100f;

    private bool _previousStateAim;
    private void Awake()
    {
        if (!instance) instance = this;
    }

    public void ShowAimTarget(bool activ)
    {
        if (!_aimSprite.enabled && WeaponCatalog.instance.CurrentWeapon.Gun.isAiming && activ) return;
        _aimSprite.enabled = activ;
    }

    public void ShowAmountDamage(float amount , Color color)
    {
        var pos = new Vector2(Random.Range(_pointSpawnUIDamage.position.x - _posSpawnRange,
            _pointSpawnUIDamage.position.x + _posSpawnRange), _pointSpawnUIDamage.position.y);
        var display = Instantiate(_uiAmountDamage, pos, Quaternion.identity);
        display.transform.SetParent(_pointSpawnUIDamage);
        display.text = amount.ToString();
        display.color = color;
    }
}
