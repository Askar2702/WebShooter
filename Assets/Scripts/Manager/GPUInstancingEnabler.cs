using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUInstancingEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (!meshRenderer) return;
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
      //  meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
    public void EnableOptimizedMesh(SkinnedMeshRenderer meshRenderer)
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
      //  meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
    
    public void EnableOptimizedMesh(MeshRenderer meshRenderer)
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
      //  meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    
}
