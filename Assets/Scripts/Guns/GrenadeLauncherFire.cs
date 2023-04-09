using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrenadeLauncherFire : FireGun
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Grenade _grenade;
    [SerializeField] private float _force;
    [SerializeField] private Transform _grenade_launcherDrum;
    float y;

    private void OnEnable()
    {
        _grenade_launcherDrum.localEulerAngles = Vector3.zero;
    }
    public override void Fire(Action callback)
    {
        if (Input.GetMouseButtonDown(0))
        {
            var grenade = Instantiate(_grenade, _firePoint.position, _firePoint.rotation);
            grenade.Init(_camera.transform.forward * _force);
            callback?.Invoke();

            _grenade_launcherDrum.localEulerAngles = Vector3.zero;
            var rot = _grenade_launcherDrum.localEulerAngles;
            y = rot.y + 60;
        }
    }

    void Update()
    {
        _grenade_launcherDrum.localEulerAngles = Vector3.RotateTowards(_grenade_launcherDrum.localEulerAngles
            , new Vector3(0, y, 0), 0f, 0.5f);

    }

   


}
