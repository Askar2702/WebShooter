using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }
    [SerializeField] private TextMeshProUGUI _AllEnemyCountText;
    [SerializeField] private TextMeshProUGUI _deadEnemyCountText;

    [Space(30)]
    [SerializeField] private RewardEffectForKill _rewardEffectForKill;
    public UnityEvent<FinishParametrs> Finished = new UnityEvent<FinishParametrs>();

    private int _deadEnemyCount;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        _AllEnemyCountText.text = EnemySpawn.instance.CountEnemy().ToString();
    }

    public void AddScore(bool isHeadShot)
    {
        _deadEnemyCount++;
        _deadEnemyCountText.text = _deadEnemyCount.ToString();
        _rewardEffectForKill.CalculateEffect(isHeadShot);
        if(_deadEnemyCount >= EnemySpawn.instance.CountEnemy())
        {
            Finished?.Invoke(_rewardEffectForKill.GetFinishParametrs());
        }
    }
}
