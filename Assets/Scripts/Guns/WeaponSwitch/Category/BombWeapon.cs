using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : WeaponParent
{
    private void OnEnable()
    {
        AnimationManager.instance.StartGrenade();
    }

    private void OnDisable()
    {
        AnimationManager.instance.EndBombThrow();
    }
}
