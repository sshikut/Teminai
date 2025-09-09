using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHandler : MonoBehaviour
{
    public AnomalyManager anomaly;

    public Transform spawnPoint;

    public CharacterController characterController;

    public void NormalButton()
    {
        if (!anomaly.isAnomaly)
        {
            anomaly.loopCount++;
        }
        else
        {
            anomaly.absentCount++;
        }

        characterController.enabled = false;

        characterController.transform.position = spawnPoint.position;
        characterController.enabled = true;

        anomaly.Anomaly();
    }
}
