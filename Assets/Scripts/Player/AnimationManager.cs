using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public AnimationState AnimationState { get; private set; }

    private void Awake()
    {
        if (!instance) instance = this;
    }


 

    public void ReloadGun()
    {
        if (AnimationState == AnimationState.Reload) return;
        AnimationState = AnimationState.Reload;
        _animator.SetInteger("PlayerState", (int)AnimationState);
        StartCoroutine(EndReload());
    }

    public void ShowAnimationWalkOrRun(float directionMagnitude, float currentSpeed, float runSpeed)
    {
        if (CheckBaseAnimation())
        {
            if (directionMagnitude >= 0.5f)
                AnimationState = currentSpeed == runSpeed ? AnimationState.Run : AnimationState.Walk;
            else AnimationState = AnimationState.Idle;

            if (_animator.GetInteger("PlayerState") == (int)AnimationState) return;
            _animator.SetInteger("PlayerState", (int)AnimationState);
        }
    }

    private bool CheckBaseAnimation()
    {
        return Input.GetMouseButton(0)==false
            && AnimationState != AnimationState.Reload &&
            AnimationState != AnimationState.ChangeGun;
    }

    public void ShowAimAnimation()
    {
        if (AnimationState == AnimationState.AimPos) return;
        AnimationState = AnimationState.AimPos;
        _animator.SetInteger("PlayerState", (int)AnimationState);
    }

    public bool isAimAnimation()
    {
        return _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == _aimGunAnimation;
    }
    private IEnumerator EndReload()
    {
        var clip = _animator.runtimeAnimatorController.animationClips.FirstOrDefault(item => item.name == _reloadGunAnimation);
        yield return new WaitForSeconds(clip.averageDuration);
        AnimationState = AnimationState.Idle;
    }

}
