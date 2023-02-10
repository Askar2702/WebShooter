using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BombInit : MonoBehaviour
{
    [SerializeField] private Bomb _grenade;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _force;

    private Bomb _currentGrenade;

    void Update()
    {
        if (AnimationManager.instance.AnimationState == AnimationState.BombThrow) return;
        if (Input.GetMouseButtonDown(0) && !_currentGrenade)
        {
            AnimationManager.instance.StartGrenade();
        }
        if (Input.GetMouseButtonUp(0))
        {
            AnimationManager.instance.BombThrow();
        }
    }

    public void BombThrow()
    {
        {
            print("g2");
            _currentGrenade = Instantiate(_grenade, _spawnPoint.position, Quaternion.identity);
            var dirforward = transform.forward * _force;
            var dirUp = transform.up;
            print(_currentGrenade);
            StartCoroutine(_currentGrenade.Init(dirforward + dirUp));
            _currentGrenade = null;
        }
    }
}
