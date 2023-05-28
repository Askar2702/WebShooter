using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player instance;
    public UnityEvent PlayerDead;
    private HealthPlayer _healthPlayer;
    [field:SerializeField] public PlayerInput playerInput { get; private set; }
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _reloadClip;
    [HideInInspector] public FireGun Gun;
    public bool IsReload { get; private set; }
    private void Awake()
    {
        _healthPlayer = GetComponent<HealthPlayer>();
        if (!instance) instance = this;
    }

    public void TakeDamage(float amount)
    {
        if (!_healthPlayer.IsAlive) return;
        _healthPlayer.TakeDamage(amount , PlayerDead);
    }

    public void ReloadGun()
    {
        if (!_healthPlayer.IsAlive) return;
        if (IsReload || WeaponCatalog.instance.CurrentWeapon.FireGun.IsFullBullets()) return;
        IsReload = true;
        StartCoroutine(Reload());
    }
    public bool Alive()
    {
        return _healthPlayer.IsAlive;
    }
    IEnumerator Reload()
    {
        AnimationManager.instance.ReloadGun();
        _audioSource.PlayOneShot(_reloadClip);
        yield return StartCoroutine(Gun.ReloadGun()); 
        IsReload = false;
    }
    
}
