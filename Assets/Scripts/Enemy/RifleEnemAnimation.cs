using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleEnemAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ShowIdle()
    {
        if (_animator.GetInteger("VectorY") == 0 && _animator.GetInteger("VectorX") == 0) return;
        _animator.SetInteger("VectorY", 0);
        _animator.SetInteger("VectorX", 0);
    }
    public void ShowWalkForward()
    {
        if (_animator.GetInteger("VectorY") == 1 && _animator.GetInteger("VectorX") == 0) return;
        _animator.SetInteger("VectorY", 1);
        _animator.SetInteger("VectorX", 0);
    }

    public void ShowWalkBack()
    {
       if (_animator.GetInteger("VectorY") == -1 && _animator.GetInteger("VectorX") == 0) return;
        _animator.SetInteger("VectorY", -1);
        _animator.SetInteger("VectorX", 0);
    }

    public void ShowWalkRight()
    {
        if (_animator.GetInteger("VectorY") == 0 && _animator.GetInteger("VectorX") == 1) return;
        _animator.SetInteger("VectorY", 0);
        _animator.SetInteger("VectorX", 1);
    }

    public void ShowWalkLeft()
    {
        if (_animator.GetInteger("VectorY") == 0 && _animator.GetInteger("VectorX") == -1) return;
        _animator.SetInteger("VectorY", 0);
        _animator.SetInteger("VectorX", -1);
    }
}
