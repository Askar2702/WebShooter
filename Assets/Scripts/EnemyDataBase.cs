using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataEnemySpawnList", menuName = "SpawnTypeEnemy")]

public class EnemyDataBase : ScriptableObject
{
    public EnemyCellList[] EnemyCellLists;
}
[System.Serializable]
public class EnemyCellList
{
    public EnemyCell[] EnemyCell;

    public int Count()
    {
        int count = 0;
        for (int i = 0; i < EnemyCell.Length; i++)
        {
            count += EnemyCell[i].Count;
        }
        return count;
    }
}


[System.Serializable]
public class EnemyCell
{
    public Enemy Enemy;
    public int Count;
}
