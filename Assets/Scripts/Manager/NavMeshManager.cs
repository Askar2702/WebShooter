using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface;



    public void Build()
    {
        _surface.BuildNavMesh();
    }
}
