using UnityEngine;
using TMPro;

public class SlotUI : MonoBehaviour
{
    [Header("AnomalyManager")]
    public AnomalyManager anomalyManager;

    [Header("�Ķ� ���� �ؽ�Ʈ��")]
    public TMP_Text loopCountText;   // ù ��° �׸� (���� Ƚ��)
    public TMP_Text absentCountText; // �� ��° �׸� (�Ἦ ��)
    public TMP_Text gradeText;       // �� ��° �׸� (����)

    private string[] grades = { "F", "D", "D+", "C", "C+", "B", "B+", "A", "A+" };

    private void Update()
    {
        if (anomalyManager == null) return;

        // ���� ǥ��
        loopCountText.text = anomalyManager.loopCount.ToString();
        absentCountText.text = anomalyManager.absentCount.ToString();

        // ���� ���
        string grade = "F";
        if (anomalyManager.loopCount < grades.Length)
        {
            grade = grades[anomalyManager.loopCount];
        }
        else
        {
            grade = grades[grades.Length - 1]; // �迭 ��(A+) ����, Ȥ�ó� �;
        }

        gradeText.text = grade;
    }
}