using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        var newPos = Player.instance.transform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90f, Player.instance.transform.eulerAngles.y, 0f);
    }
}
