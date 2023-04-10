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
    [SerializeField] private
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
}
