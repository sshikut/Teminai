using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RapidPressQTE : MonoBehaviour
{
    public FirstPersonController player;
    public Communication student;
    public GameObject pressE;
    public GameObject botherTrigger;
    public GameObject slider;
    public GameObject closedDoor;
    public GameObject leftDoor;
    public int pressesRequired = 20;

    [Header("Event")]
    public UnityEvent onQTEStart;
    public UnityEvent<float> onQTEProgress;
    public UnityEvent onQTESuccess;

    private int currentPresses = 0;
    private bool isQTEActive = false;

    public void StartQTE()
    {
        currentPresses = 0;
        isQTEActive = true;
        onQTEStart.Invoke();
        onQTEProgress.Invoke(0f);
        pressE.SetActive(true);
        slider.SetActive(true);
    }

    void Update()
    {
        if (!isQTEActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentPresses++;

            // 진행률 계산 및 이벤트 호출
            float progress = (float)currentPresses / pressesRequired;
            onQTEProgress.Invoke(progress);

            // 성공 조건 확인
            if (currentPresses >= pressesRequired)
            {
                isQTEActive = false; // QTE 비활성화
                onQTESuccess.Invoke();

                player.lockMovement = false;
                player.cameraRotation = true;
                pressE.SetActive(false);
                student.PlayDialogue("아..", 10f);
                botherTrigger.SetActive(false);
                slider.SetActive(false);
                Debug.Log("QTE 성공!");
            }
        }
    }
    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += ResetQTE;
    }

    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= ResetQTE;
    }

    public void InitClosedDoor()
    {
        if (closedDoor.activeSelf) 
        {
            closedDoor.SetActive(false);
        }
    }

    public void ResetQTE()
    {
        InitClosedDoor();
        player.lockMovement = false;
        player.cameraRotation = true;
        pressE.SetActive(false);
        botherTrigger.SetActive(false);
        slider.SetActive(false);
         
        int layerIndex = LayerMask.NameToLayer("Interactable");
        leftDoor.layer = layerIndex;
    }
}
