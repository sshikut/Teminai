using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressUI : MonoBehaviour, IPointerDownHandler
{
    public AnomalyManager anomalyManager;
    private bool hasBeenClicked = false; // Ŭ�� ���θ� �����ϴ� ����

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (hasBeenClicked)
        {
            return;
        }

      

        if (anomalyManager != null)
        {
            anomalyManager.loopCount++;
        }
        else
        {
           
            return;
        }

       
        hasBeenClicked = true;
    }

}
