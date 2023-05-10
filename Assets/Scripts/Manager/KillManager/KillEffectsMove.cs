using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class KillEffectsMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image _image;
    public void Init()
    {
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
            _image.color = new Color(_image.color.r, _image.color.b, _image.color.g, _image.color.a - Time.deltaTime);
            yield return null;
        }
    }
    
}
