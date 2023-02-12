using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;

    [SerializeField] private Animator _animator;
    private readonly string _runAnimation = "Character_Run";
    private readonly string _reloadGunAnimation = "Character_Reload";
    private readonly string _idleAnimation = "Character_Idle";
    private readonly string _walkAnimation = "Character_Walk";
    private readonly string _changeGunAnimation = "ChangeGun";
    private readonly string _aimGunAnimation = "Player_AImpose";
    private readonly string _GrenadeThrowAnimation = "GrenadeThrow";
    private readonly string _HoldTheGnarataAnimation = "HoldTheGnarata";


    public AnimationState AnimationState { get; private set; }

    public UnityEvent<AnimationState> AnimationStateEvent = new UnityEvent<AnimationState>();

    private void Awake()
    {
        if (!instance) instance = this;
    }


    private void Update()
    {
     //   print(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name + "anim");
    }

    public void ReloadGun()
    {
        if (AnimationState == AnimationState.Reload) return;
        AnimationState = AnimationState.Reload;
      //  SetfloatStatAanim(AnimationState);
        _animator.SetInteger("PlayerState", (int)AnimationState);
        StartCoroutine(EndCurrentanimAnsStartIdle(_reloadGunAnimation));
        AnimationStateEvent?.Invoke(AnimationState);
    }

    public void ShowAnimationWalkOrRun(float directionMagnitude, float currentSpeed, float runSpeed)
    {
        if (CheckBaseAnimation())
        {
            if (directionMagnitude >= 0.5f)
                AnimationState = currentSpeed == runSpeed ? AnimationState.Run : AnimationState.Walk;
            else AnimationState = AnimationState.Idle;

            AnimationStateEvent?.Invoke(AnimationState);
           // SetfloatStatAanim(AnimationState);
            if (_animator.GetInteger("PlayerState") == (int)AnimationState) return;
            _animator.SetInteger("PlayerState", (int)AnimationState);
        }
    }

    private bool CheckBaseAnimation()
    {
        return Input.GetMouseButton(0)==false
            && AnimationState != AnimationState.Reload &&
            AnimationState != AnimationState.ChangeGun
            &&
            AnimationState != AnimationState.StartGrenade &&
            AnimationState != AnimationState.BombThrow;
    }

    public void ShowAimAnimation()
    {
        if (AnimationState == AnimationState.AimPos) return;
        AnimationState = AnimationState.AimPos;
        AnimationStateEvent?.Invoke(AnimationState);
      //  SetfloatStatAanim(AnimationState);
        _animator.SetInteger("PlayerState", (int)AnimationState);
    }

    public bool isAimAnimation()
    {
        return _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == _aimGunAnimation;
    }
    private IEnumerator EndCurrentanimAnsStartIdle(string str)
    {
        var clip = _animator.runtimeAnimatorController.animationClips.FirstOrDefault(item => item.name == str);
        yield return new WaitForSeconds(clip.averageDuration);
        AnimationState = AnimationState.Idle;
        AnimationStateEvent?.Invoke(AnimationState);
       // SetfloatStatAanim(AnimationState);
        _animator.SetInteger("PlayerState", (int)AnimationState);
    }

    public void StartGrenade()
    {
        AnimationState = AnimationState.StartGrenade;
       // SetfloatStatAanim(AnimationState);
        _animator.SetInteger("PlayerState", (int)AnimationState);
    }

    public void BombThrow()
    {
       // print(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != _GrenadeThrowAnimation);
        if (AnimationState == AnimationState.StartGrenade && _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name!=_GrenadeThrowAnimation)
        {
            AnimationState = AnimationState.BombThrow;
          //  SetfloatStatAanim(AnimationState);
            _animator.SetInteger("PlayerState", (int)AnimationState);
        }
      //  StartCoroutine(EndCurrentanimAnsStartIdle(_GrenadeThrowAnimation));
    }

    public void EndBombThrow()
    {
        AnimationState = AnimationState.Idle;
       // SetfloatStatAanim(AnimationState);
        _animator.SetInteger("PlayerState", (int)AnimationState);
    }
    
   

}
