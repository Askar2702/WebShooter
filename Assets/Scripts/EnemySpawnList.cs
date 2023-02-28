using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataEnemySpawnList", menuName = "SpawnTypeEnemy")]
public class EnemySpawnList : ScriptableObject
{
    public EnemyList[] EnemyLists;
}

[System.Serializable]
public class EnemyList
{
    public Enemy Enemy;
    public int Count;
}
