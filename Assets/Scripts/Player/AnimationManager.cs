using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;

    [SerializeField] private Gun _gun;
    [SerializeField] private Animator _animator;
    private readonly string _runAnimation = "Character_Run";
    private readonly string _reloadGunAnimation = "ReloadGun";
    private readonly string _idleAnimation = "IdleRifle";
    private readonly string _walkAnimation = "WalkRifle";
    private readonly string _changeGunAnimation = "SwitchingWeapon";
    private readonly string _aimGunAnimation = "AimPosRifle";
    private readonly string _GrenadeThrowAnimation = "GrenadeThrow";
    private readonly string _HoldTheGnarataAnimation = "HoldTheGnarata";
    private PlayerInput _playerInput;

    public AnimationState AnimationState { get; private set; }
    private AnimationState _currentState;

    public UnityEvent<AnimationState> AnimationStateEvent = new UnityEvent<AnimationState>();

    private void Awake()
    {
        if (!instance) instance = this;
        _playerInput = GetComponent<PlayerInput>();
    }

    public void SetGun(Gun gun)
    {
        _gun = gun;
    }


    public void ReloadGun()
    {
        if (AnimationState == AnimationState.Reload) return;
        AnimationState = AnimationState.Reload;
        //  SetfloatStatAanim(AnimationState);
        _animator.SetInteger("PlayerState", (int)AnimationState);
        StartCoroutine(EndCurrentAnimAndStartIdle(_gun.ReloadAnimationName.name));
        AnimationStateEvent?.Invoke(AnimationState);
    }

    public void ShowAnimationWalkOrRun(float directionMagnitude, float currentSpeed, float idleSpeed)
    {
        if (CheckBaseAnimation())
        {
            if (directionMagnitude >= 1f && _playerInput.CheckGround())
            {
                if (currentSpeed > idleSpeed)
                    AnimationState = AnimationState.Run;
                else if (currentSpeed <= idleSpeed && !_gun.isAiming)
                {
                    AnimationState = AnimationState.Walk;
                }
                _animator.SetInteger("PlayerState", (int)AnimationState);
                _animator.SetFloat("MaxSpeed", directionMagnitude);
            }
            else if (directionMagnitude <= 1f && _playerInput.CheckGround() || !_playerInput.CheckGround())
            {
                ShowAimAnimation();
            }

            AnimationStateEvent?.Invoke(AnimationState);
        }
    }

    private bool CheckBaseAnimation()
    {
        return Input.GetMouseButton(0) == false
            && AnimationState != AnimationState.Reload &&
            AnimationState != AnimationState.ChangeGun
            &&
            AnimationState != AnimationState.StartGrenade &&
            AnimationState != AnimationState.BombThrow;
    }
    public bool CheckRun()
    {
        return AnimationState == AnimationState.Run;
    }

    public void ShowAimAnimation()
    {
        if (AnimationState == AnimationState.AimPos) return;
        AnimationState = AnimationState.AimPos;
        AnimationStateEvent?.Invoke(AnimationState);
        _animator.SetInteger("PlayerState", (int)AnimationState);
    }

    public bool isAimAnimation()
    {
        return _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == _gun.AimingAnimationName.name;
    }


    public void StartGrenade()
    {
        AnimationState = AnimationState.StartGrenade;
        _animator.SetInteger("PlayerState", (int)AnimationState);
    }

    public void BombThrow()
    {
        if (AnimationState == AnimationState.StartGrenade && _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != _GrenadeThrowAnimation)
        {
            AnimationState = AnimationState.BombThrow;
            _animator.SetInteger("PlayerState", (int)AnimationState);
        }
    }

    public void ShowIdleAnim()
    {
        AnimationState = AnimationState.Idle;
        _animator.SetInteger("PlayerState", (int)AnimationState);
    }

    public void SwitchingWeaponAnim(System.Action callback, AnimationState state)
    {
        _currentState = state;
        //  if (_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == _changeGunAnimation) return;
        AnimationState = AnimationState.ChangeGun;
        _animator.SetInteger("PlayerState", (int)AnimationState);
        StartCoroutine(FollowAnimationSwitchingWeapon(callback));
    }


    private IEnumerator FollowAnimationSwitchingWeapon(System.Action callback)
    {
        ResolveSwitchWeaponAnimation.instance.ResetPosAndRotate();
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == _changeGunAnimation);
      
        var clip = _animator.GetCurrentAnimatorClipInfo(0).FirstOrDefault(item => item.clip.name == _changeGunAnimation).clip;
        yield return new WaitForSeconds(clip.length / 5);
        AnimationState = _currentState;
        _animator.SetInteger("PlayerState", (int)AnimationState);
        callback?.Invoke();
    }

    private IEnumerator EndCurrentAnimAndStartIdle(string currentAnim)
    {
        var clip = _animator.runtimeAnimatorController.animationClips.FirstOrDefault(item => item.name == currentAnim);
     
        yield return new WaitUntil(() => clip.name == _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
     
        yield return new WaitForSeconds(clip.averageDuration);
      
        AnimationState = AnimationState.AimPos;
        AnimationStateEvent?.Invoke(AnimationState);
        _animator.SetInteger("PlayerState", (int)AnimationState);
      
    }





}
