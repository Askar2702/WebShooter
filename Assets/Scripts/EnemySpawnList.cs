using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataEnemySpawnList", menuName = "SpawnTypeEnemy")]
public class EnemySpawnList : ScriptableObject
{
    public EnemyList[] EnemyLists;

    public int Count()
    {
        int count = 0;
        for(int i = 0; i < EnemyLists.Length; i++)
        {
            count += EnemyLists[i].Count;
        }
        return count;
    }
}


[System.Serializable]
public class EnemyList
{
    public Enemy Enemy;
    public int Count;
}
