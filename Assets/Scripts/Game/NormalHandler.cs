using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHandler : MonoBehaviour
{
    public AnomalyManager anomaly;

    public Transform spawnPoint;

    public CharacterController characterController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!anomaly.isAnomaly)
            {
                anomaly.loopCount++;
            }
            else
            {
                anomaly.absentCount++;
            }

            characterController = other.GetComponent<CharacterController>();
            characterController.enabled = false;

            other.transform.position = spawnPoint.position;
            characterController.enabled = true;

            anomaly.Anomaly();
        }
    }
}
