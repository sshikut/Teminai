using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressUI : MonoBehaviour, IPointerDownHandler
{
    public AnomalyManager anomalyManager;
    private bool hasBeenClicked = false; // 클릭 여부를 저장하는 변수

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
