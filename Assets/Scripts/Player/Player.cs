using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private HealthPlayer _healthPlayer;
    private void Awake()
    {
        _healthPlayer = GetComponent<HealthPlayer>();
    }

    public void TakeDamage(float amount)
    {
        _healthPlayer.TakeDamage(amount);
    }
}
