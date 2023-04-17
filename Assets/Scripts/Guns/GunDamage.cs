using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDamage : MonoBehaviour
{
    public float Damage => _damage;
    [SerializeField] private float _damage;
    [SerializeField] private float _force;
    public void ShootEnemy(Enemy e, Rigidbody rb)
    {
        if (!e.IsAlive()) return;
        var enemy = e;
        var damage = _damage;
        var color = Color.black;
        if (rb.CompareTag("MinDamage"))
        {
            damage /= 2; 
        }
        else if (rb.CompareTag("BodyDamage"))
        {
            damage = _damage;
            color = Color.green;
        }
        else if (rb.CompareTag("HeadShot"))
        {
            damage *= 200f;
            color = Color.red;
        }
        enemy.TakeDamage(damage , rb.CompareTag("HeadShot"));
      
        var dir = (transform.forward
             * _force) + transform.up * (_force );
        rb.AddForce(dir, ForceMode.Impulse);
        UIManager.instance.ShowAmountDamage(enemy.GetAmountDamageDealt() , color);
    }
}
