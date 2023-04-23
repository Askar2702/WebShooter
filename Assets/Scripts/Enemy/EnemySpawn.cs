using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance { get; private set; }
   
    [SerializeField] private EnemyDataBase _enemyDataBase;
    private List<EnemyRifle> _enemyRifles = new List<EnemyRifle>();
    private List<ShieldDroneEnemy> _shieldDrones = new List<ShieldDroneEnemy>();
    private List<EnemyMelee> _enemyMelees = new List<EnemyMelee>();
    private int _index;
    private void Awake()
    {
        if (!instance) instance = this;
        StartCoroutine(SpawnEnemies());
    }
    void Start()
    {
        _index = Game.instance.Level >= _enemyDataBase.EnemyCellLists.Length 
            ? _enemyDataBase.EnemyCellLists.Length - 1 : Game.instance.Level;
        ScoreManager.instance.ShowCountEnemy(CountEnemy());
    }

 

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(0.5f);

        List<AudioSource> audioSources = new List<AudioSource>();
        for (int i = 0; i < _enemyDataBase.EnemyCellLists[_index].EnemyCell.Length; i++)
        {
            for(int j = 0; j < _enemyDataBase.EnemyCellLists[_index].EnemyCell[i].Count; j++)
            {
                var pos = PointsManager.instance.GetRandomPos().position;
                pos = new Vector3(Random.Range(pos.x - 5, pos.x + 5), pos.y, Random.Range(pos.z - 5, pos.z + 5));
                var e = Instantiate(_enemyDataBase.EnemyCellLists[_index].EnemyCell[i].Enemy, pos, Quaternion.identity);
               
                if (e.GetComponent<EnemyRifle>()) _enemyRifles.Add(e.GetComponent<EnemyRifle>());
                if (e.TryGetComponent(out ShieldDroneEnemy shieldDrone)) _shieldDrones.Add(shieldDrone);
                if (e.TryGetComponent(out EnemyMelee enemyMelee)) _enemyMelees.Add(enemyMelee);

                if (e.TryGetComponent(out AudioSource audio))
                audioSources.Add(audio);

                if (j >= _enemyDataBase.EnemyCellLists[0].EnemyCell[i].Count) e.gameObject.SetActive(false);
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

    public List<ShieldDroneEnemy> GetShieldDroneEnemies()
    {
        return _shieldDrones;
    }

    public void DeleteShield(ShieldDroneEnemy shieldDrone)
    {
        _shieldDrones.Remove(shieldDrone);
        var e = _shieldDrones.FirstOrDefault(item => item.gameObject.activeSelf == false);
        if (e) e.gameObject.SetActive(true);
    }
    public void DeletelSelf(EnemyRifle enemyRifle)
    {
        _enemyRifles.Remove(enemyRifle);
        var e = _enemyRifles.FirstOrDefault(item => item.gameObject.activeSelf == false);
        if (e) e.gameObject.SetActive(true);
    }

    public void DeleteMelle(EnemyMelee melee)
    {
        _enemyMelees.Remove(melee);
        var e =_enemyMelees.FirstOrDefault(item => item.gameObject.activeSelf == false);
        if (e) e.gameObject.SetActive(true);
    }
    public int CountEnemy()
    {
        return _enemyDataBase.EnemyCellLists[_index].Count();
    }
}
