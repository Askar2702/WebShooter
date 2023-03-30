using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeapon : FireGun
{
   

    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private DestroyAfterTimeParticle _holeWallParticle;
    [SerializeField] private ParticleSystem _hitWall;
    private float _floatInfrontOfWall = 0.1f;
    private GunDamage _gunDamage;

    private void Awake()
    {
        _gunDamage = GetComponent<GunDamage>();
    }
    public override void Fire(Action callback)
    {
        if (Input.GetMouseButton(0) && !AnimationManager.instance.isReloadAnimShow())
        {
            AnimationManager.instance.ShowAimAnimation();
            if (AnimationManager.instance.isAimAnimation())
            {
                callback?.Invoke();
                Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
                {
                    SpawnEffectHitWall(hit);

                    if (hit.transform.root.TryGetComponent(out Enemy enemy) && hit.transform.GetComponent<Rigidbody>())
                        _gunDamage.ShootEnemy(enemy, hit.transform.GetComponent<Rigidbody>());

                }
            }
        }
    }



    private void SpawnEffectHitWall(RaycastHit hit)
    {
        if (hit.collider.gameObject.layer != 9)
        {
            var hitWall = Instantiate(_hitWall, hit.point, Quaternion.identity);
            hitWall.transform.LookAt(transform.root.position);
            if (hit.collider.gameObject.layer == 6)
                Instantiate(_holeWallParticle, hit.point + hit.normal * _floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
        }
    }
}
