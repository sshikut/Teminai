using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyHandler : MonoBehaviour
{
    public AnomalyManager anomaly;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (anomaly.isAnomaly)
            {
                anomaly.loopCount++;
            }
            else
            {
                anomaly.absentCount++;
            }

            InteractionManager.Instance.StartFadeOut();

            anomaly.Anomaly();
        }
    }


}
