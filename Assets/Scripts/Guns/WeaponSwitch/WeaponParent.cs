using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponParent : MonoBehaviour
{
    [field: SerializeField] public int id { get; private set; }
    public virtual void RigOn()
    {
       
    }

    public virtual void RigOff()
    {

    }



}


