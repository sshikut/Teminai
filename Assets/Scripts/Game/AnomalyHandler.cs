using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyHandler : MonoBehaviour
{
    public AnomalyManager anomaly;
    public CharacterController player;

    private void OnTriggerEnter(Collider other)
    {
        /*
        
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

        */

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

            player.enabled = false;
            player.transform.position = new Vector3
                (player.transform.position.x, 
                player.transform.position.y + 24.309997f, 
                player.transform.position.z);

            player.enabled = true;

            anomaly.Anomaly();
        }
    }


}
