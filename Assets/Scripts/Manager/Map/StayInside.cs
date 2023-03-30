using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{
    private MiniMapCamera _minimapCam;
    [SerializeField] private float _minimapSize;
    private Vector3 _tempV3;
    private void Awake()
    {
        _minimapCam = FindObjectOfType<MiniMapCamera>();
        if (!_minimapCam) enabled = false;
    }
    void Update()
    {
        _tempV3 = transform.parent.transform.position;
        _tempV3.y = transform.position.y;
        transform.position = _tempV3;
    }

    void LateUpdate()
    {
        // Center of Minimap
        Vector3 centerPosition = _minimapCam.transform.localPosition;

        // Just to keep a distance between Minimap camera and this Object (So that camera don't clip it out)
        centerPosition.y -= 0.5f;

        // Distance from the gameObject to Minimap
        float Distance = Vector3.Distance(transform.position, centerPosition);

        // If the Distance is less than MinimapSize, it is within the Minimap view and we don't need to do anything
        // But if the Distance is greater than the MinimapSize, then do this
        if (Distance > _minimapSize)
        {
            // Gameobject - Minimap
            Vector3 fromOriginToObject = transform.position - centerPosition;

            // Multiply by MinimapSize and Divide by Distance
            fromOriginToObject *= _minimapSize / Distance;

            // Minimap + above calculation
            transform.position = centerPosition + fromOriginToObject;
        }
    }
}
