using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInstance<T> : MonoBehaviour
{
    public static T instance { get; protected set; }

   
}

