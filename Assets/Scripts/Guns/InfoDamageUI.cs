using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfoDamageUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rect = GetComponent<RectTransform>();
        rect.DOAnchorPos(new Vector2(rect.anchoredPosition.x, 150f), 0.5f)
            .SetLink(gameObject).OnComplete(() => { Destroy(gameObject); });
    }

   
}
