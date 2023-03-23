using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Linq;

public class BaseWeapon : WeaponParent
{
    [SerializeField] private Animator _baseAnimator;
    [SerializeField] private RuntimeAnimatorController _animatorController;
    [SerializeField] private Rig _rig;
    [SerializeField] private RigBuilder _rigBuilder;

    public override void RigOn()
    {
        _baseAnimator.runtimeAnimatorController = _animatorController;
        RigLayer rigLayer = new RigLayer(_rig);
        _rig.weight = 1;
        _rigBuilder.layers.Add(rigLayer);
        _rigBuilder.Build();
    }
    public override void RigOff()
    {
        _rig.weight = 0;
        _rigBuilder.layers.Remove(_rigBuilder.layers.FirstOrDefault(item => item.rig == _rig));
    }
}
