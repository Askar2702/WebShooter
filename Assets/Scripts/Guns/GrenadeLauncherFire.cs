using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncherFire : FireGun
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Grenade _grenade;
    [SerializeField] private float _force;
    public override void Fire(Action callback)
    {
        if (Input.GetMouseButtonDown(0))
        {
            var grenade = Instantiate(_grenade, _firePoint.position, _firePoint.rotation);
            grenade.Init(_camera.transform.forward * _force);
            callback?.Invoke();
        }
    }
}
