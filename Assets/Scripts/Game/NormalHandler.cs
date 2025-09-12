using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHandler : MonoBehaviour
{
    public AnomalyManager anomaly;
    public MovableObject moveonEnable;
    
    public void NormalButton()
    {
        GameManager.Instance.audioManager.Play("Decision2");

        if (!anomaly.isAnomaly)
        {
            anomaly.loopCount++;
        }
        else
        {
            anomaly.absentCount++;
        }

        InteractionManager.Instance.StartFadeOut();

        moveonEnable.ResetPosition(); // ��ư������ ��ũ�� ����ġ
        anomaly.Anomaly();
       
    }
}
