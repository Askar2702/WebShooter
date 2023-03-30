using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RewardEffectForKill : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _headShotSound;
    [SerializeField] private KillEffectsMove _iconHeadShot;
    [SerializeField] private RectTransform _spawmPosHeadShot;

    [SerializeField] private AudioClip _doubleKillSound;

    [SerializeField] private AudioClip _tripleKillSound;

    [SerializeField] private AudioClip _monsterKillSound;

    [SerializeField] private AudioClip _rampageSound;

    [SerializeField] private float _waitingTime = 0.5f;

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
        if (_currentCount >= 5) _audioSource.PlayOneShot(_rampageSound);
        switch (_currentCount)
        {
            case 2:
                _audioSource.PlayOneShot(_doubleKillSound);
                break;
            case 3:
                _audioSource.PlayOneShot(_tripleKillSound);
                break;
            case 4:
                _audioSource.PlayOneShot(_monsterKillSound);
                break;
        }
        _currentCount = 0;
    }
}
