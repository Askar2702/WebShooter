using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class KillEffectsText : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _text;
    public void Init(string t)
    {
        _text.text = t;
        StartCoroutine(UpdateProcess());
        transform.DOScale(2f, 0.5f).SetEase(Ease.OutBack);
        Destroy(gameObject, 3f);
    }

    IEnumerator UpdateProcess()
    {
        yield return new WaitForSeconds(1f);
        transform.DOScale(0.0f, 0.5f);
        while (true)
        {
            _text.color = new Color(_text.color.r, _text.color.b, _text.color.g, _text.color.a - Time.deltaTime);
            yield return null;
        }
    }
}
