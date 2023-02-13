using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCatalog : MonoBehaviour
{
    public static WeaponCatalog instance;
    public int WeaponsCount => WeaponsCatalog.Length;
    private WeaponParent[] WeaponsCatalog = new WeaponParent[2];
   [field:SerializeField] public WeaponParent CurrentWeapon { get; private set; }

    [SerializeField] private PistolWeapon _pistol;
    [SerializeField] private BaseWeapon _baseWeapon;
    [SerializeField] private TechnoWeapon _technoWeapon;
    [SerializeField] private BombWeapon _bomb;

    private void Awake()
    {
        if (!instance) instance = this;
        CurrentWeapon = _baseWeapon;
      //  WeaponsCatalog[0] = _pistol;
        WeaponsCatalog[0] = _baseWeapon;
       // WeaponsCatalog[2] = _technoWeapon;
        WeaponsCatalog[1] = _bomb;
        CurrentWeapon.gameObject.SetActive(true);
    }

    public void SelectWeapon(int indexWeapon)
    {
        int i = 0;
        foreach (var weapon in WeaponsCatalog)
        { 
            if (indexWeapon != i && weapon != null)
            {
                weapon.gameObject.SetActive(false);
            }
            if (indexWeapon == i && weapon != null)
            {
                weapon.gameObject.SetActive(true);
                CurrentWeapon = weapon;
            }
            else if(indexWeapon == i && weapon == null)
            {
                CurrentWeapon = null;
            }
            i++;
        }
        SwitchingWeaponShowAnim();
    }

    private void SwitchingWeaponShowAnim()
    {
        if (CurrentWeapon == null) return;
        AnimationState state;
        if (CurrentWeapon.GetType() == typeof(BaseWeapon)) state = AnimationState.Idle;
       // if (CurrentWeapon.GetType() == typeof(TechnoWeapon)) state = AnimationState.Idle;
       // if (CurrentWeapon.GetType() == typeof(PistolWeapon)) state = AnimationState.Idle;
        else state = AnimationState.StartGrenade;
        AnimationManager.instance.SwitchingWeaponAnim(state);
    }


}
