using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _count;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < _count; i++)
        {
            var pos = MazeSpawner.instance.GetRandomPos();
            var e = Instantiate(_enemy, pos, Quaternion.identity);
        }
    }
}
