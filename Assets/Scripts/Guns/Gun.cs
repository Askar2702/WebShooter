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
   
    [SerializeField] private float _interval;


    [SerializeField] private DestroyAfterTimeParticle _holeWallParticle;
    [SerializeField] private ParticleSystem _hitWall;
   
    [SerializeField] private Recoil _recoil;
    [SerializeField] private CameraRecoil _cameraRecoil;
    private bool isReady;
    private float _floatInfrontOfWall = 0.1f;


    [SerializeField] private ParticleSystem _muzzleFalshEffect;
    [SerializeField] private AudioSource _fireSound;

    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _aimPos;

    [Space(30)]
    [SerializeField] private Vector3 _gunPosAiming;
    [SerializeField] private Vector3 _gunRotAiming;

  

    
    [field: SerializeField] public AnimationClip AimingAnimationName { get; private set; }

    [Space(25)]
    [SerializeField] private Camera _aimimgCamera;
    [Range(1, 25)]
    [SerializeField] private float _opticSize;
    public bool isAiming { get; private set; }

    private Camera _currentCam;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        isReady = true;
        isAiming = false;
        AnimationManager.instance.AnimationStateEvent.AddListener(CheckAnimation);
        _currentCam = Camera.main;
        _aimimgCamera.fieldOfView = _opticSize;
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
                AnimationManager.instance.ShowAimAnimation();
                Aiming();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Cursor.lockState = CursorLockMode.Locked;

            }
        }
    }

    private void Fire()
    {
       
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
        Ray ray = _currentCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
        {
            SpawnEffectHitWall(hit);
            
            if (hit.transform.root.TryGetComponent(out Enemy enemy) && hit.transform.GetComponent<Rigidbody>())
                _gunDamage.ShootEnemy(enemy, hit.transform.GetComponent<Rigidbody>());
            
        }
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
           
            transform.localPosition = _gunPosAiming;
            transform.localEulerAngles = _gunRotAiming;
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
        _parent.DOLocalMove(pos, 0.05f);
        isAiming = isAim;
        UIManager.instance.ShowAimTarget(!isAim);
    }

    private void SpawnEffectHitWall(RaycastHit hit)
    {
        if (hit.collider.gameObject.layer != 9)
        {
            var hitWall = Instantiate(_hitWall, hit.point, Quaternion.identity);
            hitWall.transform.LookAt(transform.root.position);
            if(hit.collider.gameObject.layer == 6)
                Instantiate(_holeWallParticle, hit.point + hit.normal * _floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
        }
    }


   

    private void OnDisable()
    {
        MoveAimPos(_startPos, false);
    }
}
