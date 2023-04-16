using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance { get; private set; }
   
    [SerializeField] private EnemySpawnList _spawnDataList;
    private List<EnemyRifle> _enemyRifles = new List<EnemyRifle>();
    private void Awake()
    {
        if (!instance) instance = this;
        StartCoroutine(SpawnEnemies());
    }
    void Start()
    {
       
    }

 

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(0.5f);

        List<AudioSource> audioSources = new List<AudioSource>();
        for (int i = 0; i < _spawnDataList.EnemyLists.Length; i++)
        {
            for(int j = 0; j < _spawnDataList.EnemyLists[i].Count; j++)
            {
                var pos = PointsManager.instance.GetRandomPos().position;
                pos = new Vector3(Random.Range(pos.x - 5, pos.x + 5), pos.y, Random.Range(pos.z - 5, pos.z + 5));
                var e = Instantiate(_spawnDataList.EnemyLists[i].Enemy, pos, Quaternion.identity);
                if (e.GetComponent<EnemyRifle>()) _enemyRifles.Add(e.GetComponent<EnemyRifle>());

                if(e.TryGetComponent(out AudioSource audio))
                audioSources.Add(audio);
            }
            
        }
        if (SettingGame.instance)
        {
            SettingGame.instance.SetSoundVolume(audioSources.ToArray());
        }

    }

    public List<EnemyRifle> GettingEnemyRifles()
    {
        return _enemyRifles;
    }

    internal void DeletelSelf(EnemyRifle enemyRifle)
    {
        _enemyRifles.Remove(enemyRifle);
    }

    public int CountEnemy()
    {
        return _spawnDataList.Count();
    }
}
