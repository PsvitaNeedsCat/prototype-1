using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LapCheckpoint : MonoBehaviour
{
    public int checkpointNum = 0;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            player.PassedCheckpoint(checkpointNum);
        }
    }
}
