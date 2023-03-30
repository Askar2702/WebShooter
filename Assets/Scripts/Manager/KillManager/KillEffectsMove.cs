using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class KillEffectsMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _image;
    void Start()
    {
        StartCoroutine(UpdateProcess());
        transform.DOScale(1.5f, 0.5f).SetEase(Ease.OutBack);
        _rectTransform.DOAnchorPos(new Vector2(0, 300f), 3f);
        Destroy(gameObject, 3f);
    }

    IEnumerator UpdateProcess()
    {
        while (true)
        {
            _image.color = new Color(_image.color.r, _image.color.b, _image.color.g, _image.color.a - Time.deltaTime);
            yield return null;
        }
    }
    
}
