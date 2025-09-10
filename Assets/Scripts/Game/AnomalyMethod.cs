using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyMethod : MonoBehaviour
{
    [Header("Anomaly #1")]
    public GameObject changSubGod;

    [Header("Anomaly #2")]
    public GameObject Show_Anomaly2;

    [Header("Anomaly #3")]
    public GameObject Show_Anomaly3;

    [Header("Anomaly #4")]
    public GameObject Show_Anomaly4;

    [Header("Anomaly #5")]
    public GameObject Show_Anomaly5;

    [Header("Anomaly #6")]
    public GameObject Show_Anomaly6;

    [Header("Anomaly #7")]
    public GameObject Show_Anomaly7;
    public GameObject hide_Anomaly1;

    public void InitAnomaly()
    {
        changSubGod.SetActive(false);
    }

    public void Anomaly_1()
    {
        changSubGod.SetActive(true);
    }
    public void InitAnomaly2()
    {
        Show_Anomaly2.SetActive(true);
    }

    public void Anomaly_2()
    {
        Show_Anomaly2.SetActive(false);
    }
    public void InitAnomaly3()
    {
        Show_Anomaly3.SetActive(true);
    }

    public void Anomaly_3()
    {
        Show_Anomaly3.SetActive(false);
    }
    public void InitAnomaly4()
    {
        Show_Anomaly4.SetActive(true);
    }

    public void Anomaly_4()
    {
        Show_Anomaly4.SetActive(false);
    }
    public void InitAnomaly5()
    {
        Show_Anomaly5.SetActive(true);
    }

    public void Anomaly_5()
    {
        Show_Anomaly5.SetActive(false);
    }
    public void InitAnomaly6()
    {
        Show_Anomaly6.SetActive(true);
    }

    public void Anomaly_6()
    {
        Show_Anomaly6.SetActive(false);
    }

    public void InitAnomaly7()
    {
        Show_Anomaly7.SetActive(true);
        hide_Anomaly1.SetActive(false);
    }

    public void Anomaly_7()
    {
        Show_Anomaly7.SetActive(false);
        hide_Anomaly1.SetActive(true);
    }
}
