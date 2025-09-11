using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    public CharacterController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.enabled = false;
            player.transform.position = new Vector3
                (player.transform.position.x,
                player.transform.position.y - 24.309997f,
                player.transform.position.z);

            player.enabled = true;
        }
    }
}
