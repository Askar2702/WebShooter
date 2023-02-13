using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BombInit : WeaponParent
{
    [SerializeField] private Bomb _grenade;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _force;

    private Bomb _currentGrenade;

    void Update()
    {
        // print(AnimationManager.instance.AnimationState);
        if (WeaponCatalog.instance.CurrentWeapon
            && WeaponCatalog.instance.CurrentWeapon.GetType() == typeof(BombWeapon)
            && AnimationManager.instance.AnimationState == AnimationState.StartGrenade)
        {


            if (Input.GetMouseButtonUp(0))
            {
                AnimationManager.instance.BombThrow();
            }
        }
    }

    public void BombThrow()
    {
        _currentGrenade = Instantiate(_grenade, _spawnPoint.position, Quaternion.identity);
        var dirforward = transform.forward * _force;
        var dirUp = transform.up;
        StartCoroutine(_currentGrenade.Init(dirforward + dirUp));
        _currentGrenade = null;
    }

    public void EndBombThrow()
    {
        AnimationManager.instance.StartGrenade();
    }
}
