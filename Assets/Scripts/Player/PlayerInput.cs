using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _angleX;
    [SerializeField] private float _idleSpeed = 3.0f;
    [SerializeField] private float _runSpeed = 5.0f;
    [SerializeField] private float _speedRotate;
    public float _speedCurrentRotate;
    private float _currentSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _gravity;
    private float _yaw = 0f;
    private float _pitch = 0f;
    private Vector3 dir;

    [Space(30)]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _walkClip;
    [SerializeField] private AudioClip _runClip;

    private void Start()
    {
        _currentSpeed = _idleSpeed;
        _speedCurrentRotate = _speedRotate;
        Cursor.visible = false;
        SettingGame.instance.SetSoundVolume();
        if (SettingGame.instance.SpeedCamera != 0)
            _speedRotate = SettingGame.instance.SpeedCamera;
        SettingGame.instance.ChangeSpeed.AddListener((float speed) =>
        {
            _speedRotate = speed;
            AimingChungeSpeed(WeaponCatalog.instance.CurrentWeapon.Gun.isAiming);
        });
        
    }
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.R))
        {
            Player.instance.ReloadGun();
        }
       
        Rotate();
        Move();

        if (transform.position.y < -10f) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
    }

    private void Rotate()
    {
        if (Time.timeScale == 0) return;
        _pitch -= _speedCurrentRotate * Input.GetAxis("Mouse Y");
        _yaw += _speedCurrentRotate * Input.GetAxis("Mouse X");
        _pitch = Mathf.Clamp(_pitch, -_angleX, _angleX);
        transform.eulerAngles = new Vector3(_pitch, _yaw, 0);
    }

    private void Move()
    {
        Vector2 movementInput = GetMovementInput();

        if (movementInput.y > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _currentSpeed = _runSpeed;
            }
        }
        if (!CheckGround() || movementInput.x != 0 || movementInput.y < 0 || movementInput == Vector2.zero
            || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.R))
        {
            _currentSpeed = _idleSpeed;
        }

        if (movementInput != Vector2.zero && CheckGround())
        {
            if (_currentSpeed == _runSpeed) _audioSource.clip = _runClip;

            else _audioSource.clip = _walkClip;
            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else if (movementInput == Vector2.zero || !CheckGround())
        {
            _audioSource.Stop();
        }

        if (CheckGround())
        {
            dir = new Vector3(movementInput.x * _currentSpeed, dir.y, movementInput.y * _currentSpeed);
            dir = transform.TransformDirection(dir);

            if (Input.GetButtonDown("Jump"))
            {
                dir.y = _jumpSpeed;
            }
            else
            {
                dir = new Vector3(dir.x, 0, dir.z);
            }
        }

        dir.y -= _gravity * Time.deltaTime;

        if (movementInput == Vector2.zero)
        {
            dir = new Vector3(0f, dir.y, 0f);
        }

        _characterController.Move(dir * Time.deltaTime);

        AnimationManager.instance.ShowAnimationWalkOrRun(dir.magnitude, _currentSpeed, _idleSpeed);
    }

    private Vector2 GetMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector2(horizontal, vertical);
    }

    public bool CheckGround()
    {
        return _characterController.isGrounded;
    }

    public void AimingChungeSpeed(bool isAim)
    {
        if (isAim) _speedCurrentRotate  = _speedRotate / 2;
        else _speedCurrentRotate = _speedRotate;
    }
}
