using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHave : MonoBehaviour
{
    public static WeaponHave instance { get; private set; }
    private List<WeaponParent> _weaponParents = new List<WeaponParent>();
    private void Awake()
    {
       if(!instance) instance = this;
        DontDestroyOnLoad(this);
    }

    public void AddWeapon(WeaponParent weapon)
    {
        if (!_weaponParents.Contains(weapon))
            _weaponParents.Add(weapon);
    }
    public List<WeaponParent> GetWeapons()
    {
        return _weaponParents;
    }
    
}
