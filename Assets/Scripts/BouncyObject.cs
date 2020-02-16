using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player)
        {
            Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
            playerRigidbody.velocity = -playerRigidbody.velocity;
        }
    }
}
