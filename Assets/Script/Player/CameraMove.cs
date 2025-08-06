using System;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static Func<Vector3> playerPosition;

    private void FixedUpdate()
    {
        transform.position = new Vector3(playerPosition().x, playerPosition().y + 13, playerPosition().z - 10.25f);
    }
}
