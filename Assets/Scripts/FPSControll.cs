using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FPSControll : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;
    private float _poollingTime = 1f;
    private float _time;
    private int _countFrame;

    void Update()
    {
        _time += Time.deltaTime;
        _countFrame++;
        if (_time >= _poollingTime)
        {
            int frameRate = Mathf.RoundToInt(_countFrame / _time);
            _fpsText.text = $"FPS : {frameRate}";
            _time -= _poollingTime;
            _countFrame = 0;
        }
    }
}
