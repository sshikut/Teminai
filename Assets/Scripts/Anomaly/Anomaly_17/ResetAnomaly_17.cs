using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnomaly_17 : MonoBehaviour
{
    public GameObject snowman;
    public Transform resetPosition;
    public SnowManButton button;

    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += ResetSnowman;
    }

    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= ResetSnowman;
    }

    public void ResetSnowman()
    {
        snowman.transform.position = resetPosition.position;
        snowman.SetActive(false);
        button.isOn = false;
    }
}
