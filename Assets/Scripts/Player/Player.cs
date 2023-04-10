using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private HealthPlayer _healthPlayer;
    [field:SerializeField] public PlayerInput playerInput { get; private set; }
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _reloadClip;
    [HideInInspector] public FireGun Gun;
    private bool _isReload;
    private void Awake()
    {
        _healthPlayer = GetComponent<HealthPlayer>();
        if (!instance) instance = this;
    }

    public void TakeDamage(float amount)
    {
        _healthPlayer.TakeDamage(amount);
    }

    public void ReloadGun()
    {
        if (_isReload || WeaponCatalog.instance.CurrentWeapon.FireGun.IsFullBullets()) return;
        _isReload = true;
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        AnimationManager.instance.ReloadGun();
        _audioSource.PlayOneShot(_reloadClip);
        yield return StartCoroutine(Gun.ReloadGun()); 
        _isReload = false;
    }
    
}
