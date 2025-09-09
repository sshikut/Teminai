using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHandler : MonoBehaviour
{
    public AnomalyManager anomaly;

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

        InteractionManager.instance.StartFadeOut();

        anomaly.Anomaly();
    }
}
