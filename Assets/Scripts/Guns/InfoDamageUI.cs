using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfoDamageUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().DOAnchorPos(Vector2.up * 100f, 0.5f)
            .SetLink(gameObject).OnComplete(()=> { Destroy(gameObject); });
    }

   
}
