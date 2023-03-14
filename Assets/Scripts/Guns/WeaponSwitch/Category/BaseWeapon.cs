using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BaseWeapon : WeaponParent
{
    [SerializeField] private Animator _baseAnimator;
    [SerializeField] private RuntimeAnimatorController _animatorController;
    [SerializeField] private Rig _rig;

    private void OnEnable()
    {
        _rig.weight = 1;
        _baseAnimator.runtimeAnimatorController = _animatorController;
    }
    private void OnDisable()
    {
        _rig.weight = 0;
    }
}
