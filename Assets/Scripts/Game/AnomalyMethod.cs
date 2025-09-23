using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyMethod : MonoBehaviour
{
    [Header("Interactable Objects")]
    // 상호작용이 가능한 오브젝트들을 초기화하기 위한 배열

    public GameObject[] interactable;


    [Header("Anomaly #1")]
    public GameObject changSubGod;

    [Header("Anomaly #2")]
    public GameObject show_Anomaly2;

    [Header("Anomaly #3")]
    public GameObject show_Anomaly3;

    [Header("Anomaly #4")]
    public GameObject show_Anomaly4;

    [Header("Anomaly #5")]
    public GameObject show_Anomaly5;

    [Header("Anomaly #6")]
    public GameObject show_Anomaly6;

    [Header("Anomaly #7")]
    public GameObject show_Anomaly7;
    public GameObject hide_Anomaly1;

    public TriggerZone triggerZone1;

    [Header("Anomaly #8")] // 급똥 이상 현상
    public GameObject anomaly8_ToiletPaper;

    public void InitAnomaly()
    {
        changSubGod.SetActive(false);
        triggerZone1.triggerOnce = false;
        triggerZone1.hasTriggered = false;
        
        // show_Anomaly2.SetActive(true);
        // show_Anomaly3.SetActive(true);
        show_Anomaly4.SetActive(true);
        show_Anomaly5.SetActive(true);
        show_Anomaly6.SetActive(true);
        show_Anomaly7.SetActive(true);
        
        hide_Anomaly1.SetActive(false);

        anomaly8_ToiletPaper.SetActive(false);

        Debug.Log("메소드" + triggerZone1.triggerOnce);
    }

    public void InitInteractable()
    {

    }

    public void Anomaly_1()
    {
        changSubGod.SetActive(true);
    }

    public void Anomaly_2()
    {
        show_Anomaly2.SetActive(false);
    }

    public void Anomaly_3()
    {
        show_Anomaly3.SetActive(false);
    }

    public void Anomaly_4()
    {
        show_Anomaly4.SetActive(false);
    }

    public void Anomaly_5()
    {
        show_Anomaly5.SetActive(false);
    }

    public void Anomaly_6()
    {
        show_Anomaly6.SetActive(false);
    }

    public void Anomaly_7()
    {
        show_Anomaly7.SetActive(false);
        hide_Anomaly1.SetActive(true);
    }

    public void Anomaly_8() // 급똥
    {
        anomaly8_ToiletPaper.SetActive(true);
    } 
   
}
