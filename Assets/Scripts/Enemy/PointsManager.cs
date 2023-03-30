using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager  : MonoBehaviour
{
    public static PointsManager instance;
    [SerializeField] private Transform[] _points;

    private void Awake()
    {
        if (!instance) instance = this;
    }
    public Transform GetRandomPos()
    {
        return _points[Random.Range(0, _points.Length)];
    }

   
}
