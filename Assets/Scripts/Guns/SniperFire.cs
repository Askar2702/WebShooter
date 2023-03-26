using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperFire : FireGun
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private DestroyAfterTimeParticle _holeWallParticle;
    [SerializeField] private ParticleSystem _hitWall;
    private float _floatInfrontOfWall = 0.1f;
    private GunDamage _gunDamage;
    [SerializeField] private bool isAcross;



    private void Awake()
    {
        _gunDamage = GetComponent<GunDamage>();
    }
    public override void Fire(Action callback)
    {
        if (Input.GetMouseButtonDown(0))
        {
            AnimationManager.instance.ShowAimAnimation();
            // if (AnimationManager.instance.isAimAnimation())
            {
                callback?.Invoke();
                Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                if (isAcross)
                {
                    RaycastHit[] hit;
                    hit = Physics.RaycastAll(ray, Mathf.Infinity, _layerMask);
                    foreach (var h in hit)
                    {
                        SpawnEffectHitWall(h);

                        if (h.transform.root.TryGetComponent(out Enemy enemy) && h.transform.GetComponent<Rigidbody>())
                            _gunDamage.ShootEnemy(enemy, h.transform.GetComponent<Rigidbody>());
                    }
                }
                else
                {
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
