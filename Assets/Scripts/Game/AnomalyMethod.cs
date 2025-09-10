using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyMethod : MonoBehaviour
{
    [Header("Anomaly #1")]
    public GameObject changSubGod;

    public void InitAnomaly()
    {
        changSubGod.SetActive(false);
    }

    public void Anomaly_1()
    {
        changSubGod.SetActive(true);
    }
}
