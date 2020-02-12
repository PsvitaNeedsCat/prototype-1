using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            if (player.IsRespawning) { return; }
            player.SetInputControl(false);
            float respawnTime = player.Respawn();
            StartCoroutine(EnablePlayerInput(respawnTime, player, true));
        }
    }

    private IEnumerator EnablePlayerInput(float afterSeconds, Player player, bool canInput)
    {
        yield return new WaitForSeconds(afterSeconds);

        player.SetInputControl(canInput);
    }
}
