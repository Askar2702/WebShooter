using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDamage : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _force;
    public void ShootEnemy(Enemy e, Rigidbody rb)
    {
        if (!e.IsAlive()) return;
        var enemy = e;
        var damage = _damage;
        var color = Color.white;
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
        enemy.TakeDamage(damage);
        //var dir = (transform.forward
        //    + new Vector3(Random.Range(-transform.right.x, transform.right.x), transform.right.y, transform.right.z)) * _force;
        var dir = (transform.forward
             * _force) + transform.up * (_force );
        rb.AddForce(dir, ForceMode.Impulse);
        UIManager.instance.ShowAmountDamage(enemy.GetAmountDamageDealt() , color);
    }
}
