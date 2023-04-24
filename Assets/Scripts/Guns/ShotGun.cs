using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShotGun : FireGun
{
    private Vector3 _spreadVector = new Vector3(0.5f, 0.5f, 0.5f);
    private int _spreadCount = 5;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

  
   
    public override void Fire(Action callback)
    {
        if (Input.GetMouseButtonDown(0) && !AnimationManager.instance.isReloadAnimShow() && _countBulletsCurrent > 0)
        {
            AnimationManager.instance.ShowAimAnimation();
          //  if (AnimationManager.instance.isAimAnimation())
            {
                callback?.Invoke();
                RaycastHit hit;
                for (int i = 0; i < _spreadCount; i++)
                {
                    Vector3 direction = _camera.transform.forward * 10;
                    direction += new Vector3(UnityEngine.Random.Range(-_spreadVector.x, _spreadVector.x),
                        UnityEngine.Random.Range(-_spreadVector.y, _spreadVector.y),
                        UnityEngine.Random.Range(-_spreadVector.z, _spreadVector.z));
                    direction.Normalize();

                    if (Physics.Raycast(_camera.transform.position, direction, out hit, Mathf.Infinity, _layerMask))
                    {
                        SpawnEffectHitWall(hit);
                        _gunDamage.ShootEnemy(hit);
                    }
                }
            }
            base.Fire(callback);
        }
    }



    
}
