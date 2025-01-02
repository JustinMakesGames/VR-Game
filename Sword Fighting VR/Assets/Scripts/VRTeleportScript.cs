using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VRTeleportScript
{
    public static void TeleportVrPlayer(Transform player, Vector3 endPosition)
    {
        player.position = endPosition;
        player.parent.GetChild(1).position = endPosition;
        player.parent.GetChild(2).position = endPosition;
    }
}