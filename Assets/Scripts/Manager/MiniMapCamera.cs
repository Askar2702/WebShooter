using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField] private PlayerInput _player;
    private void LateUpdate()
    {
        var newPos = _player.transform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        // transform.rotation = Quaternion.Euler(90f, _player.transform.eulerAngles.y, 0f);
    }
}
