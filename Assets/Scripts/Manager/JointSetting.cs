using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointSetting : MonoBehaviour
{
    [SerializeField] private CharacterJoint[] _characterJoints;
    void Start()
    {
        foreach(var j in _characterJoints)
        {
            j.enableProjection = true;
            j.enablePreprocessing = true;
        }
    }

  
}
