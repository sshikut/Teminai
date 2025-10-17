using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RapidPressQTE : MonoBehaviour
{
    public GameObject pressE;
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
                Debug.Log("QTE 성공!");
            }
        }
    }
}
