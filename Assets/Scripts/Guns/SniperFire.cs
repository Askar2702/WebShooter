using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperFire : FireGun
{
    [SerializeField] private Camera _camera; 
    [SerializeField] private LayerMask _layerMask;

   
    [SerializeField] private bool isAcross;



   
    public override void Fire(Action callback)
    {
        if (Input.GetMouseButtonDown(0) && !AnimationManager.instance.isReloadAnimShow() && _countBulletsCurrent > 0)
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
            base.Fire(callback);
        }

      
    }



   
}
