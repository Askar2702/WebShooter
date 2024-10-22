using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _parent;

   
    [SerializeField] private float _interval;


    [SerializeField] private DestroyAfterTimeParticle _holeWallParticle;
    [SerializeField] private ParticleSystem _hitWall;
   
    private Recoil _recoil;
    private CameraRecoil _cameraRecoil;
    private bool isReady;
    
    private FireGun _fireGun;


    [SerializeField] private ParticleSystem _muzzleFalshEffect;
    [SerializeField] private AudioSource _fireSound;

    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _aimPos;

    private SniperFire _sniperFire;



    [field: SerializeField] public AnimationClip AimingAnimationName { get; private set; }
    [field: SerializeField] public AnimationClip ReloadAnimationName { get; private set; }

    [Space(25)]
    [SerializeField] private Camera _aimimgCamera;
    [Range(1, 25)]
    [SerializeField] private float _opticSize;
    public bool isAiming { get; private set; }

 

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _recoil = GetComponent<Recoil>();
        _cameraRecoil = GetComponent<CameraRecoil>();
        _fireGun = GetComponent<FireGun>();
        _sniperFire = GetComponent<SniperFire>();
    }

    private void OnEnable()
    {
        isReady = true;
        isAiming = false;
        _aimimgCamera.fieldOfView = _opticSize;
    }
    private void Start()
    {
        AnimationManager.instance.AnimationStateEvent.AddListener(CheckAnimation);
    }


    // Update is called once per frame
    void Update()
    {
        if (isReady && Time.timeScale!= 0 && Player.instance.Alive() && !Player.instance.IsReload)
        {
            Fire();
        }
        if (Input.GetMouseButtonDown(1) && !Player.instance.IsReload)
        {
            AnimationManager.instance.ShowAimAnimation();
            Aiming();
        }
       
    }

    private void Fire()
    {
        _fireGun.Fire(ShowEffectFire);
    }

    private void ShowEffectFire()
    {
        {
            _muzzleFalshEffect.Play();
            _fireSound.Play();
            _recoil.RecoilFire(isAiming);
            _cameraRecoil.RecoilCamera(isAiming);
            isReady = false;
            StartCoroutine(ResetReady());
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
            AimingChungeSpeed(false);
        }
        else 
        {
           
            MoveAimPos(_aimPos, true);
            AimingChungeSpeed(true);
        }

    }

    private void CheckAnimation(AnimationState animationState)
    {
        if (animationState == AnimationState.Reload || animationState == AnimationState.Run && isAiming)
        {
            MoveAimPos(_startPos, false);
            AimingChungeSpeed(false);
        }
    }

    private void MoveAimPos(Vector3 pos , bool isAim)
    {
        _parent.DOLocalMove(pos, 0.05f);
        isAiming = isAim;
        UIManager.instance.ShowAimTarget(!isAim);
        AimingChungeSpeed(false);

    }

 
    private void AimingChungeSpeed(bool isAim)
    {
        if (_sniperFire != null)
        {
           Player.instance.playerInput.AimingChungeSpeed(isAim);
        }
    }

    


    private void OnDisable()
    {
        if (!this.gameObject.scene.isLoaded) return;
        MoveAimPos(_startPos, false);
    }
}
