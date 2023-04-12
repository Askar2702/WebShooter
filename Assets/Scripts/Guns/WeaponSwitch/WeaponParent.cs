using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    [field: SerializeField] public int id { get; private set; }
    [field:SerializeField] public FireGun FireGun { get; private set; }
    [field: SerializeField] public Gun Gun { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }


    public virtual void RigOn()
    {
       
    }

    public virtual void RigOff()
    {

    }



}


