using UnityEngine;
using TMPro;
using System.Collections;


public class TimerManager : MonoBehaviour
{
    public float timeRemaining = 30f; // 초기 제한 시간
    public TMP_Text timerText;
    public GameObject GoalTrigger; // 목표 지점 트리거
    public AnomalyManager anomaly;
    private bool isTimerActive = false;

    // 초기 설정 시간을 저장할 변수 추가
    private float initialTimeRemaining;

    void Awake()
    {
        // Awake에서 초기 시간을 저장합니다.
        initialTimeRemaining = timeRemaining;
    }

    void Update()
    {
        if (isTimerActive)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                EndGame(false); // 시간 초과로 인한 게임 오버

            }
        }
    }

    public void StartTimer()
    {
        isTimerActive = true;
        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
        }
        if (GoalTrigger != null)
        {
            GoalTrigger.SetActive(true);
        }
    }

    public void StopTimer(bool success)
    {
        EndGame(success);
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("남은 시간: {0:00}:{1:00}", minutes, seconds);
    }

    private void EndGame(bool success)
    {
        isTimerActive = false;
        if (timerText != null)
        {
            timerText.text = success ? ""+anomaly.loopCount++ : ""+anomaly.absentCount++; 
        }
        InteractionManager.Instance.StartFadeOut();
        
        anomaly.Anomaly();
        TogglePhone togglePhone = FindObjectOfType<TogglePhone>();
        if (togglePhone != null)
        {
            togglePhone.DisableToggleAfterOneUse();
        }
        ResetTimer();
        


        
       
    }

   
    public void ResetTimer()
    {
       
        timeRemaining = initialTimeRemaining;
        GoalTrigger.SetActive(false);
        
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
            timerText.text = ""; 
        }

       
        isTimerActive = false;
    }
}