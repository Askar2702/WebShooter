using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCatalog : MonoBehaviour
{
    public int WeaponsCount => WeaponsCatalog.Length;
    private WeaponParent[] WeaponsCatalog = new WeaponParent[4];

    [SerializeField] private PistolWeapon _pistol;
    [SerializeField] private BaseWeapon _baseWeapon;
    [SerializeField] private TechnoWeapon _technoWeapon;
    [SerializeField] private BombWeapon _bomb;

    private void Awake()
    {
        WeaponsCatalog[0] = _pistol;
        WeaponsCatalog[1] = _baseWeapon;
        WeaponsCatalog[2] = _technoWeapon;
        WeaponsCatalog[3] = _bomb;
    }

    public void SelectWeapon(int indexWeapon)
    {
        int i = 0;
        foreach (var weapon in WeaponsCatalog)
        {
            if (indexWeapon == i && weapon != null) weapon.gameObject.SetActive(true);
            else if (indexWeapon != i && weapon != null) weapon.gameObject.SetActive(false);
            i++;
        }
    }


}
