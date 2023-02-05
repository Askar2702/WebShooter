using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private PlayerBullet _playerBullet;
    [SerializeField] private Transform _spawnBulletPoint;
    [SerializeField] private float _interval;

    [SerializeField] private DestroyAfterTimeParticle _timeParticle;
    private Vector3 _direction;
    private Recoil _recoil;
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
        _recoil = GetComponent<Recoil>();
        isReady = true;
        isAiming = false;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && isReady)
        {
            AnimationManager.instance.ShowAimAnimation();
            if (AnimationManager.instance.isAimAnimation())
                Fire();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

    private void Fire()
    {
        var b = Instantiate(_playerBullet, _spawnBulletPoint.position, _spawnBulletPoint.rotation);

        _muzzleFalshEffect.Play();

        //  b.Init( ray.direction);
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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask)) {
            _direction = hit.point;
            Instantiate(_timeParticle, hit.point + hit.normal * _floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
        }

        else _direction = ray.direction * 500;
        _spawnBulletPoint.LookAt(_direction);
    }

    IEnumerator ResetReady()
    {
        yield return new WaitForSeconds(_interval);
        isReady = true;
    }

    public void Aiming()
    {
        if (isAiming)
        {
            transform.DOLocalMove(_startPos, 0.5f);
            isAiming = false;
        }
        else 
        {
            transform.DOLocalMove(_aimPos, 0.5f);
            isAiming = true;
        }
    }
   
}
