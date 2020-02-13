using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
    public float boostForce = 100.0f;
    public Vector3 boostDir = Vector3.forward;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player)
        {
            // player.ApplyImpulse(boostForce * (transform.rotation * boostDir.normalized));
            player.ApplyForce(boostForce * (transform.rotation * boostDir.normalized));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.rotation * boostDir.normalized);
        Gizmos.DrawWireSphere(transform.position + (transform.rotation * boostDir.normalized), 0.1f);
    }
}
