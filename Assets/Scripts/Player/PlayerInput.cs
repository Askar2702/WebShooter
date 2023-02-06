using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _angleX;
    [SerializeField] private float _idleSpeed = 3.0f;
    [SerializeField] private float _runSpeed = 5.0f;
    [SerializeField] private float _speedRotate = 1f;
    private float _currentSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _gravity;
    private float _yaw = 0f;
    private float _pitch = 0f;
    private Vector3 dir;

    [SerializeField] private Gun _gun;
    [SerializeField] private Image _aimSprite;

    // Update is called once per frame
    private void Start()
    {
        _currentSpeed = _idleSpeed;
        Cursor.visible = false;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R)) AnimationManager.instance.ReloadGun();
        if (Input.GetMouseButtonDown(1))
        {
            _gun.Aiming();
            _aimSprite.enabled = !_aimSprite.enabled;
            AnimationManager.instance.ShowAimAnimation();
        }
        Rotate();
        Move();

    }

    private void Rotate()
    {
        _pitch -= _speedRotate * Input.GetAxis("Mouse Y");
        _yaw += _speedRotate * Input.GetAxis("Mouse X");
        _pitch = Mathf.Clamp(_pitch, -_angleX, _angleX);
        transform.eulerAngles = new Vector3(_pitch, _yaw, 0);
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift)) _currentSpeed = _runSpeed;
        else _currentSpeed = _idleSpeed;
        if (CheckGroud())
        {
            dir = new Vector3(Input.GetAxis("Horizontal") * _currentSpeed, dir.y, Input.GetAxis("Vertical") * _currentSpeed);
            dir = transform.TransformDirection(dir);
            if (Input.GetButtonDown("Jump")) dir.y = _jumpSpeed;
            else
                dir = new Vector3(dir.x, 0, dir.z);
        }

        dir.y -= _gravity * Time.deltaTime;
        _characterController.Move(dir  * Time.deltaTime);

        AnimationManager.instance.ShowAnimationWalkOrRun(dir.magnitude, _currentSpeed, _runSpeed);
        
    }

    private bool CheckGroud()
    {
        return _characterController.isGrounded;
    }
}
