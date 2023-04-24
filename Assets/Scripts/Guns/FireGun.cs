using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FireGun : MonoBehaviour
{
    [SerializeField] protected int _countBullets;
     protected int _countBulletsCurrent;
    [SerializeField] protected TextMeshProUGUI _countBulletsText;
    [SerializeField] protected TextMeshProUGUI _countBulletsTextCurrent;

    protected float _floatInfrontOfWall = 0.1f;
    [SerializeField] protected DestroyAfterTimeParticle _holeWallParticle;
    [SerializeField] protected ParticleSystem _hitWall;

    protected GunDamage _gunDamage;

    protected void Awake()
    {
        _gunDamage = GetComponent<GunDamage>();
    }
    protected void OnEnable()
    {
        _countBulletsCurrent = _countBullets;
        _countBulletsText.text = _countBullets.ToString();
        _countBulletsTextCurrent.text = _countBulletsCurrent.ToString();
    }
    public virtual void Fire(Action callback)
    {
        _countBulletsCurrent--;
        if (_countBulletsCurrent <= 0)
        {
            _countBulletsCurrent = 0;
            Player.instance.ReloadGun();
        }
        _countBulletsTextCurrent.text = _countBulletsCurrent.ToString();
    }

    public IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(2f);
        _countBulletsCurrent = _countBullets;
        _countBulletsTextCurrent.text = _countBulletsCurrent.ToString();
    }

    public bool IsFullBullets()
    {
        return _countBulletsCurrent == _countBullets;
    }

    protected virtual void SpawnEffectHitWall(RaycastHit hit)
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
