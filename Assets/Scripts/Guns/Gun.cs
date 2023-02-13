using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Gun : WeaponParent
{
    [SerializeField] private Transform _parent;

    [SerializeField] private GunDamage _gunDamage;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private PlayerBullet _playerBullet;
    [SerializeField] private Transform _spawnBulletPoint;
    [SerializeField] private float _interval;


    [SerializeField] private DestroyAfterTimeParticle _timeParticle;
    [SerializeField] private ParticleSystem _hitWall;
    private Vector3 _direction;
    [SerializeField] private Recoil _recoil;
    [SerializeField] private CameraRecoil _cameraRecoil;
    private bool isReady;
    private float _floatInfrontOfWall = 0.1f;


    [SerializeField] private ParticleSystem _muzzleFalshEffect;
    [SerializeField] private AudioSource _fireSound;

    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _aimPos;
    private bool isAiming;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        isReady = true;
        isAiming = false;
        AnimationManager.instance.AnimationStateEvent.AddListener(CheckAnimation);
    }


    // Update is called once per frame
    void Update()
    {
        if(WeaponCatalog.instance.CurrentWeapon &&
            WeaponCatalog.instance.CurrentWeapon.GetType() == typeof(BaseWeapon))
        {
            if (Input.GetMouseButton(0) && isReady)
            {
                AnimationManager.instance.ShowAimAnimation();
                if (AnimationManager.instance.isAimAnimation())
                    Fire();
            }
            if (Input.GetMouseButtonDown(1))
            {
                Aiming();
                AnimationManager.instance.ShowAimAnimation();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Cursor.lockState = CursorLockMode.Locked;

            }
        }
    }

    private void Fire()
    {
        Instantiate(_playerBullet, _spawnBulletPoint.position, _spawnBulletPoint.rotation);

        _muzzleFalshEffect.Play();

        _fireSound.Play();
        _recoil.RecoilFire(isAiming);
        _cameraRecoil.RecoilCamera(isAiming);
        isReady = false;

        ComputeDirection();
        StartCoroutine(ResetReady());
    }

    private void ComputeDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
        {
            _direction = hit.point;
            SpawnEffectHitWall(hit);
            
            if (hit.transform.root.TryGetComponent(out Enemy enemy))
                _gunDamage.ShootEnemy(enemy, hit.transform.GetComponent<Rigidbody>());
            
        }
        else 
            _direction = Vector3.zero;

        if (_direction != Vector3.zero)
            _spawnBulletPoint.LookAt(_direction);
    }
   


    IEnumerator ResetReady()
    {
        yield return new WaitForSeconds(_interval);
        isReady = true;
    }

    private void Aiming()
    {
        if (isAiming)
        {
            MoveAimPos(_startPos, false);
        }
        else 
        {
            MoveAimPos(_aimPos, true);
        }
    }

    private void CheckAnimation(AnimationState animationState)
    {
        if (animationState == AnimationState.Reload || animationState == AnimationState.Run && isAiming)
        {
            MoveAimPos(_startPos, false);
        }
    }

    private void MoveAimPos(Vector3 pos , bool isAim)
    {
        _parent.DOLocalMove(pos, 0.5f);
        isAiming = isAim;
        UIManager.instance.ShowAimTarget(!isAim);
    }

    private void SpawnEffectHitWall(RaycastHit hit)
    {
        if (hit.collider.gameObject.layer == 6)
        {
            Instantiate(_timeParticle, hit.point + hit.normal * _floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
            var hitWall = Instantiate(_hitWall, hit.point, Quaternion.identity);
            hitWall.transform.LookAt(transform.root.position);
        }
    }


   

    private void OnDisable()
    {
        //Ended();
        MoveAimPos(_startPos, false);
    }
}
