using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMenu : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    void Start()
    {
        StartCoroutine(UpdateWeapon());
    }

    IEnumerator UpdateWeapon()
    {
        while (true)
        {
            transform.Rotate(Vector3.up / _speed);
            yield return null;
        }
    }
}
