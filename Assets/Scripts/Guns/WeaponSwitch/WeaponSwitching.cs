using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int _indexWeapon;
    private int _previousIndex;
    [SerializeField] private WeaponCatalog _weaponCatalog;
    [SerializeField] private AudioSource _audio;

    private void Start()
    {
        WeaponCatalog.instance.GrenadeRemove.AddListener(() => 
        { 
            _indexWeapon = 0;
            SelectWeapon();
        });
    }
    private void Update()
    {
        if (Time.timeScale == 0 || !Player.instance.Alive()) return;
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
        if (Input.GetKeyDown(KeyCode.Alpha1)) _indexWeapon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) _indexWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) _indexWeapon = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) _indexWeapon = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) _indexWeapon = 4;



        if (_previousIndex != _indexWeapon) SelectWeapon();
    }
    private void SelectWeapon()
    {
        if (_weaponCatalog.IndexCurrentWeapon == _indexWeapon) return;
        _weaponCatalog.SelectWeapon(_indexWeapon , _audio);
    }
}
