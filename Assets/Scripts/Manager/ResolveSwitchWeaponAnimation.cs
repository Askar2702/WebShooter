using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolveSwitchWeaponAnimation : MonoBehaviour
{
    public static ResolveSwitchWeaponAnimation instance { get; private set; }

    [SerializeField] private Transform _LeftShoulder;
    private Vector3 _posLeftShoulder;
    private Vector3 _rotateLeftShoulder;

    [SerializeField] private Transform _LeftArm;
    private Vector3 _posLeftArm;
    private Vector3 _rotateLeftArm;

    [SerializeField] private Transform _LeftForeArm;
    private Vector3 _posLeftForeArm;
    private Vector3 _rotateLeftForeArm;

    [SerializeField] private Transform _LeftHand;
    private Vector3 _posLeftHand;
    private Vector3 _rotateLeftHand;

    [SerializeField] private Transform _RightShoulder;
    private Vector3 _posRightShoulder;
    private Vector3 _rotateRightShoulder;

    [SerializeField] private Transform _RightArm;
    private Vector3 _posRightArm;
    private Vector3 _rotateRightArm;

    [SerializeField] private Transform _RightForeArm;
    private Vector3 _posRightForeArm;
    private Vector3 _rotateRightForeArm;

    private void Awake()
    {
        if (!instance) instance = this;
    }
    private void Start()
    {
        _posLeftShoulder = _LeftShoulder.localPosition;
        _rotateLeftShoulder = _LeftShoulder.localEulerAngles;


        _posLeftArm = _LeftArm.localPosition;
        _rotateLeftArm = _LeftArm.localEulerAngles;


        _posLeftForeArm = _LeftForeArm.localPosition;
        _rotateLeftForeArm = _LeftForeArm.localEulerAngles;


        _posLeftHand = _LeftHand.localPosition;
        _rotateLeftHand = _LeftHand.localEulerAngles;


        _posRightShoulder = _RightShoulder.localPosition;
        _rotateRightShoulder = _RightShoulder.localEulerAngles;


        _posRightArm = _RightArm.localPosition;
        _rotateRightArm = _RightArm.localEulerAngles;


        _posRightForeArm = _RightForeArm.localPosition;
        _rotateRightForeArm = _RightForeArm.localEulerAngles;
    }

    public void ResetPosAndRotate()
    {
        _LeftShoulder.localPosition = _posLeftShoulder;
        _LeftShoulder.localEulerAngles = _rotateLeftShoulder;


        _LeftArm.localPosition = _posLeftArm;
        _LeftArm.localEulerAngles = _rotateLeftArm;


        _LeftForeArm.localPosition = _posLeftForeArm;
        _LeftForeArm.localEulerAngles = _rotateLeftForeArm;


        _LeftHand.localPosition = _posLeftHand;
        _LeftHand.localEulerAngles = _rotateLeftHand;


        _RightShoulder.localPosition = _posRightShoulder;
        _RightShoulder.localEulerAngles = _rotateRightShoulder;


        _RightArm.localPosition = _posRightArm;
        _RightArm.localEulerAngles = _rotateRightArm;


        _RightForeArm.localPosition =_posRightForeArm;
        _RightForeArm.localEulerAngles =_rotateRightForeArm;

    }
}
