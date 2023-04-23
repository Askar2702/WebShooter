using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperFire : FireGun
{
    [SerializeField] private Camera _camera; 
    [SerializeField] private LayerMask _layerMask;

   
    [SerializeField] private bool isAcross;
    [SerializeField] private Gun _gun;
    private Vector3 _spreadVector = new Vector3(20f, 20f, 20f);


    public override void Fire(Action callback)
    {
        Vector3 direction = _camera.transform.forward * 100;
        direction += new Vector3(UnityEngine.Random.Range(-_spreadVector.x, _spreadVector.x),
            UnityEngine.Random.Range(-_spreadVector.y, _spreadVector.y),
            UnityEngine.Random.Range(-_spreadVector.z, _spreadVector.z));
        direction.Normalize();

        if (Input.GetMouseButtonDown(0) && !AnimationManager.instance.isReloadAnimShow() && _countBulletsCurrent > 0)
        {
            AnimationManager.instance.ShowAimAnimation();
            {
                callback?.Invoke();
               
                if (isAcross)
                {
                    RaycastHit[] hit;
                    if (_gun.isAiming) direction = _camera.transform.forward * 100;
                     hit = Physics.RaycastAll(_camera.transform.position, direction, Mathf.Infinity, _layerMask);
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
                    if(_gun.isAiming)  direction = _camera.transform.forward * 100;
                    if (Physics.Raycast(_camera.transform.position , direction, out hit, Mathf.Infinity, _layerMask))
                    {
                        SpawnEffectHitWall(hit);

                        if (hit.transform.root.TryGetComponent(out Enemy enemy) && hit.transform.GetComponent<Rigidbody>())
                            _gunDamage.ShootEnemy(enemy, hit.transform.GetComponent<Rigidbody>());

                    }
                }
            }
            base.Fire(callback);
        }

      
    }



   
}
