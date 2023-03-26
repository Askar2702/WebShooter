using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private HealthPlayer _healthPlayer;
    [field:SerializeField] public PlayerInput playerInput { get; private set; }
    private void Awake()
    {
        _healthPlayer = GetComponent<HealthPlayer>();
        if (!instance) instance = this;
    }

    public void TakeDamage(float amount)
    {
        _healthPlayer.TakeDamage(amount);
    }
}
