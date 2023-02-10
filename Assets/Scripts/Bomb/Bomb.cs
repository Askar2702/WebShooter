using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _time;
    private bool isEnd;
    [SerializeField] private Rigidbody _rb;

   
    public IEnumerator Init(Vector3 dir)
    {
        _rb.AddForce(dir, ForceMode.Force);
        yield return new WaitForSeconds(_time);
        yield return transform.DOScale(0.5f, 0.1f).SetLink(gameObject).WaitForCompletion();
        Destroy(this.gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Enemy enemy))
        {
            var dir = enemy.transform.position - transform.position;
            // enemy.EnabledRigidBodys();
            if (other.GetComponent<Rigidbody>())
                other.GetComponent<Rigidbody>().AddForce(dir * _force, ForceMode.Impulse);
        }
    }


   
}
