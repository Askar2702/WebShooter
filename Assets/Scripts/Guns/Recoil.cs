using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    [SerializeField] private Transform _bodyParent;
    [SerializeField] private float recoilAmount_z_non = 0.5f;
    [SerializeField] private float recoilAmount_x_non = 0.5f;
    [SerializeField] private float recoilAmount_y_non = 0.5f;

    [SerializeField] private float recoilAmount_z_ = 0.5f;
    [SerializeField] private float recoilAmount_x_ = 0.5f;
    [SerializeField] private float recoilAmount_y_ = 0.5f;

    [SerializeField] private float _snappiness = 6f;
    [SerializeField] private float _returnSpeed = 0.5f;

    private Vector3 _currentRotation;
    private Vector3 _targetRotation;
   
   

   
    void FixedUpdate()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappiness * Time.fixedDeltaTime);
        _bodyParent.localRotation = Quaternion.Euler(_currentRotation);
    }

    public void RecoilFire( bool isAim)
    {
        if (!isAim)
        {
            _targetRotation += new Vector3(recoilAmount_x_non, Random.Range(-recoilAmount_y_non, recoilAmount_y_non),
            Random.Range(-recoilAmount_z_non, recoilAmount_z_non));
        }
        else
        {
            _targetRotation += new Vector3(recoilAmount_x_, Random.Range(-recoilAmount_y_, recoilAmount_y_),
        Random.Range(-recoilAmount_z_, recoilAmount_z_));
        }
    }
}
