using UnityEngine;
using TMPro;

public class SlotUI : MonoBehaviour
{
    [Header("AnomalyManager")]
    public AnomalyManager anomalyManager;

    [Header("파란 슬롯 텍스트들")]
    public TMP_Text loopCountText;   // 첫 번째 네모 (루프 횟수)
    public TMP_Text absentCountText; // 두 번째 네모 (결석 수)
    public TMP_Text gradeText;       // 세 번째 네모 (학점)

    private string[] grades = { "F", "D", "D+", "C", "C+", "B", "B+", "A", "A+" };

    private void Update()
    {
        if (anomalyManager == null) return;

        // 숫자 표시
        loopCountText.text = anomalyManager.loopCount.ToString();
        absentCountText.text = anomalyManager.absentCount.ToString();

        // 학점 계산
        string grade = "F";
        if (anomalyManager.loopCount < grades.Length)
        {
            grade = grades[anomalyManager.loopCount];
        }
        else
        {
            grade = grades[grades.Length - 1]; // 배열 끝(A+) 고정, 혹시나 싶어서
        }

        gradeText.text = grade;
    }
}