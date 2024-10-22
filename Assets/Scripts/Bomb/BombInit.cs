using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;

public class BombInit : WeaponParent
{
    [SerializeField] private Bomb _grenade;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _force;
    [SerializeField] private int _countGrenade = 1;


    [Space(30)]
    protected int _countGrenadeCurrent;
    [SerializeField] protected TextMeshProUGUI _countBulletsText;
    [SerializeField] protected TextMeshProUGUI _countBulletsTextCurrent;
  

    private Bomb _currentGrenade;

    private void Start()
    {
        _countGrenadeCurrent = _countGrenade;
    }


    void Update()
    {
        // print(AnimationManager.instance.AnimationState);
        if (WeaponCatalog.instance.CurrentWeapon
            && WeaponCatalog.instance.CurrentWeapon.GetType() == typeof(BombWeapon)
            && AnimationManager.instance.AnimationState == AnimationState.StartGrenade && Time.timeScale == 1 &&
             Player.instance.Alive())
        {

            if (Input.GetMouseButtonUp(0) && _countGrenadeCurrent > 0)
            {
                AnimationManager.instance.BombThrow();
                _countGrenadeCurrent--;
            }
            _countBulletsText.text = _countGrenade.ToString();
            _countBulletsTextCurrent.text = _countGrenadeCurrent.ToString();
        }
    }

    public void BombThrow()
    {
        _currentGrenade = Instantiate(_grenade, _spawnPoint.position, Quaternion.identity);
        var dirforward = transform.forward * _force;
        var dirUp = transform.up;
        StartCoroutine(_currentGrenade.Init(dirforward + dirUp));
        _currentGrenade = null;
        WeaponCatalog.instance.EnabledBomb(false);
    }

    /// <summary>
    /// ��� �������� ��� ��������� � ���������
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayPistolShow()
    {
        yield return new WaitForSeconds(0.5f);
        WeaponCatalog.instance.DeleteGrenade();
    }

    public void EndBombThrow()
    {
        AnimationManager.instance.StartGrenade();
        if (_countGrenadeCurrent <= 0) StartCoroutine(DelayPistolShow());
        else
            StartCoroutine(ShowBomb());
    }

    IEnumerator ShowBomb()
    {
        yield return new WaitForSeconds(0.2f);
        if (WeaponCatalog.instance.CurrentWeapon
           && WeaponCatalog.instance.CurrentWeapon.GetType() == typeof(BombWeapon)
           && AnimationManager.instance.AnimationState == AnimationState.StartGrenade)
            WeaponCatalog.instance.EnabledBomb(true);
    }


   
    
  
   

   

  
}
