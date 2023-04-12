using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class WeaponCatalog : MonoBehaviour
{
    public static WeaponCatalog instance;
    public int WeaponsCount => WeaponCurrentCatalog.Length;

    [SerializeField] private WeaponParent[] weaponCatalogs;

    private WeaponParent[] WeaponCurrentCatalog;

    [Space(30)]
    [SerializeField] private Image[] WeaponCurrentCatalogIcons;
    [field:SerializeField] public WeaponParent CurrentWeapon { get; private set; }

    [field: SerializeField] public PistolWeapon Pistol { get; private set; }
    [field: SerializeField] public BombWeapon Bomb { get; private set; }
    private int _baseCountWeapons = 2;

    private void Awake()
    {
        if (!instance) instance = this;
        CurrentWeapon = Pistol;
        CurrentWeapon.RigOn();
        WeaponInit();
        CurrentWeapon.gameObject.SetActive(true);
    }
    private void Start()
    {
        AnimationManager.instance.SetGun(CurrentWeapon.Gun);
        Player.instance.Gun = CurrentWeapon.FireGun;
    }
    public void SelectWeapon(int indexWeapon)
    {
        if (indexWeapon >= WeaponCurrentCatalog.Length) return;
        int i = 0;
        foreach (var weapon in WeaponCurrentCatalog)
        {
           
            if (indexWeapon == i && weapon != null)
            {
                CurrentWeapon = weapon;
                Player.instance.Gun = CurrentWeapon.FireGun;
                CurrentWeapon.RigOn();
            }
            if (indexWeapon != i && weapon != null)
            {
                weapon.RigOff();
                weapon.gameObject.SetActive(false);
            }
            else if (indexWeapon == i && weapon == null)
            {
                CurrentWeapon = null;
            }
            i++;
        }
        SwitchingWeaponShowAnim();
    }
    private void ShowWeapon()
    {
        CurrentWeapon.gameObject.SetActive(true);
        AnimationManager.instance.SetGun(CurrentWeapon.Gun);
    }
    private void SwitchingWeaponShowAnim()
    {
        if (CurrentWeapon == null) return;
        AnimationState state;
        if (CurrentWeapon.GetType() == typeof(BaseWeapon))
        { 
            state = AnimationState.Idle;
        }
      
        else if (CurrentWeapon.GetType() == typeof(PistolWeapon))
        {
            state = AnimationState.Idle;
        }
        else state = AnimationState.StartGrenade;
        AnimationManager.instance.SwitchingWeaponAnim(ShowWeapon , state);
    }

    public void EnabledBomb(bool activ)
    {
        Bomb.gameObject.SetActive(activ);
    }

    private void WeaponInit()
    {
        WeaponCurrentCatalog = new WeaponParent[WeaponHave.instance.GetWeapons().Count + _baseCountWeapons];
        WeaponCurrentCatalog[0] = Pistol;
        WeaponCurrentCatalog[1] = Bomb;
        var weapons = WeaponHave.instance.GetWeapons();
        for (int i = 0; i < WeaponHave.instance.GetWeapons().Count; i++)
        {
            WeaponCurrentCatalog[_baseCountWeapons + i] = weaponCatalogs[weapons[i].id];
            WeaponCurrentCatalogIcons[i].GetComponent<Image>().sprite = weaponCatalogs[weapons[i].id].Icon;
            WeaponCurrentCatalogIcons[i].transform.parent.gameObject.SetActive(true);
            //  WeaponCurrentCatalogIcons.FirstOrDefault(item => item.id == weapons[i].id).gameObject.SetActive(true);
        }
    }
}
