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
            pos = new Vector3(Random.Range(pos.x - 5, pos.x + 5), pos.y, Random.Range(pos.z - 5, pos.z + 5));
            var e = Instantiate(_enemy, pos, Quaternion.identity);
        }
    }
}
