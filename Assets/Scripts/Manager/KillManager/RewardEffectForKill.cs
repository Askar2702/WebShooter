using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RewardEffectForKill : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [Space(30)]
    [SerializeField] private AudioClip _headShotSound;
    [SerializeField] private KillEffectsMove _iconHeadShot;
    [SerializeField] private RectTransform _spawmPosHeadShot;

    [Space(10)]
    [SerializeField] private AudioClip _doubleKillSound;
    [SerializeField] private string _doubleKillText;

    [Space(10)]
    [SerializeField] private AudioClip _tripleKillSound;
    [SerializeField] private string _tripleKillText;

    [Space(10)]
    [SerializeField] private AudioClip _monsterKillSound;
    [SerializeField] private string _monsterKillText;

    [Space(10)]
    [SerializeField] private AudioClip _rampageSound;
    [SerializeField] private string _rampageText;

    [Space(30)]
    [SerializeField] private float _waitingTime = 0.5f;
    [SerializeField] private KillEffectsText _killEffects;
    [SerializeField] private RectTransform _spawnText;

    private int _currentCount;
    public void CalculateEffect(bool isHeadShot)
    {
        _currentCount++;
        if (isHeadShot)
        {
            _audioSource.PlayOneShot(_headShotSound);
            var icon = Instantiate(_iconHeadShot, _spawmPosHeadShot.position, Quaternion.identity);
            icon.transform.parent = _spawmPosHeadShot.transform;
           
        }
        StartCoroutine(Expectation(isHeadShot));
    }

    private IEnumerator Expectation(bool isHeadShot)
    {
        yield return new WaitForSeconds(_waitingTime);
        var effectT = Instantiate(_killEffects, _spawnText.position, Quaternion.identity);
        effectT.transform.parent = _spawnText.transform;
        var effectText = "";
        
        switch (_currentCount)
        {
            case 2:
                _audioSource.PlayOneShot(_doubleKillSound);
                effectText = _doubleKillText;
                break;
            case 3:
                _audioSource.PlayOneShot(_tripleKillSound);
                effectText = _tripleKillText;
                break;
            case 4:
                _audioSource.PlayOneShot(_monsterKillSound);
                effectText = _monsterKillText;
                break;
        }
        if (_currentCount >= 5)
        {
            _audioSource.PlayOneShot(_rampageSound);
            effectText = _rampageText;
        }
        effectT.Init(effectText);
        _currentCount = 0;
    }
}
