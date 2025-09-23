using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHandler : MonoBehaviour
{
    public AnomalyManager anomaly;

    public void NormalButton()
    {
        if (InteractionManager.Instance != null && InteractionManager.Instance.IsFading)
            return;

        AudioManager.instance.Play("Decision2");

        if (!anomaly.isAnomaly)
        {
            anomaly.loopCount++;
        }
        else
        {
            anomaly.absentCount++;
        }

        AudioManager.instance.Play("Decision2");

        InteractionManager.Instance.StartFadeOut();

        anomaly.Anomaly();
       
    }
}
