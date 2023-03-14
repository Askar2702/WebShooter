using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCatalog : MonoBehaviour
{
    public static WeaponCatalog instance;
    public int WeaponsCount => WeaponsCatalog.Length;
    private WeaponParent[] WeaponsCatalog = new WeaponParent[2];
   [field:SerializeField] public WeaponParent CurrentWeapon { get; private set; }

    [field: SerializeField] public PistolWeapon Pistol { get; private set; }
    [field: SerializeField] public BaseWeapon BaseWeapon { get; private set; }
    [field: SerializeField] public TechnoWeapon TechnoWeapon { get; private set; }
    [field: SerializeField] public BombWeapon Bomb { get; private set; }

    private void Awake()
    {
        if (!instance) instance = this;
        CurrentWeapon = BaseWeapon;
      //  WeaponsCatalog[0] = _pistol;
        WeaponsCatalog[0] = BaseWeapon;
       // WeaponsCatalog[2] = _technoWeapon;
        WeaponsCatalog[1] = Bomb;
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
               // weapon.gameObject.SetActive(true);
                CurrentWeapon = weapon;
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
    }
    private void SwitchingWeaponShowAnim()
    {
        if (CurrentWeapon == null) return;
        AnimationState state;
        if (CurrentWeapon.GetType() == typeof(BaseWeapon)) state = AnimationState.Idle;
       // if (CurrentWeapon.GetType() == typeof(TechnoWeapon)) state = AnimationState.Idle;
       // if (CurrentWeapon.GetType() == typeof(PistolWeapon)) state = AnimationState.Idle;
        else state = AnimationState.StartGrenade;
        AnimationManager.instance.SwitchingWeaponAnim(ShowWeapon , state);
    }

    public void EnabledBomb(bool activ)
    {
        Bomb.gameObject.SetActive(activ);
    }

}
