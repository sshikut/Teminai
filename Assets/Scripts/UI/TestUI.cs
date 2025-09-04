using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TestUI : MonoBehaviour { 

    public AnomalyManager anomalyManager;
    public TMP_Text loopCountText;
    public TMP_Text absentText;
    public TMP_Text gradeText;
    public TMP_Text timeText;

    private void Update()
    {
        UpdateText();
        UpdateTime();
    }

    public void UpdateText()
    {
        string[] grades = { "F", "D", "D+", "C", "C+", "B", "B+", "A", "A+" };
        string grade = "";

        if (anomalyManager.loopCount < grades.Length)
            grade = grades[anomalyManager.loopCount];

        string Send_loopCountText = "" + anomalyManager.loopCount;
        string Send_gradeText = grade;
        string Send_absentText = "" + anomalyManager.absentCount;

        loopCountText.text = Send_loopCountText;
        gradeText.text = Send_gradeText;
        absentText.text = Send_absentText;

    }
    private void UpdateTime()
    {
       
        if (timeText != null)
        {
            string currentTime = System.DateTime.Now.ToString("HH:mm:ss");
            timeText.text = currentTime;
        }
    }
}
