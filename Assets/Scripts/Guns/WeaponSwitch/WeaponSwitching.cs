using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int _indexWeapon;
    private int _previousIndex;
    [SerializeField] private WeaponCatalog _weaponCatalog;

    private void Update()
    {
        _previousIndex = _indexWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (_indexWeapon >= _weaponCatalog.WeaponsCount - 1) _indexWeapon = 0;
            else
                _indexWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (_indexWeapon <= 0) _indexWeapon = _weaponCatalog.WeaponsCount - 1;
            else
                _indexWeapon--;
        }



        if (_previousIndex != _indexWeapon) SelectWeapon();
    }
    private void SelectWeapon()
    {
        _weaponCatalog.SelectWeapon(_indexWeapon);
    }
}