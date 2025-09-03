using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
using UnityEngine.SceneManagement; 

public class PhoneController : MonoBehaviour
{
    // Unity �ν����� â���� ������ �޴��� UI �г� GameObject
    public GameObject phoneUiPanel;

    // ������ ǥ���� TextMeshPro UI ���
    public TextMeshProUGUI gradeDisplayText;

    // '+' ������ ������ ��ü ���� ü�踦 ���ڿ� �迭�� �����մϴ�.
    private readonly string[] gradeScale = { "F", "D", "D+", "C", "C+", "B", "B+", "A", "A+" };

    // �迭�� �ش�Ǵ� ��ȣ
    private int currentGradeIndex = 0;

    void Start()
    {
        // ������ ���۵� �� �޴��� UI�� ������ �ʵ��� ��Ȱ��ȭ�մϴ�.
        if (phoneUiPanel != null)
        {
            phoneUiPanel.SetActive(false);
        }

        // ���� ǥ�� UI�� ������Ʈ�մϴ�.
        UpdateGradeUI();
    }

    void Update()
    {
        // �� �����Ӹ��� 'Tab' Ű �Է��� �ִ��� Ȯ���մϴ�.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Ű�� ���ȴٸ� UI ��� �Լ��� ȣ���մϴ�.
            TogglePhoneUI();
        }
    }

    public void TogglePhoneUI()
    {
        if (phoneUiPanel != null)
        {
            // ���� Ȱ��ȭ ���¸� �����ͼ� �� �ݴ� ������ �����մϴ�.
            bool isActive = phoneUiPanel.activeSelf;
            phoneUiPanel.SetActive(!isActive);

            // UI�� ���� ������ ���� ������ �ֽ� ���·� ������Ʈ�մϴ�.
            if (!isActive)
            {
                UpdateGradeUI();
            }
        }
    }


    void UpdateGradeUI()
    {
        if (gradeDisplayText != null)
        {
            // ���� �ε����� �ش��ϴ� ���� ���ڿ��� �迭���� ã�ƿ� UI�� ǥ���մϴ�.
            gradeDisplayText.text = "���� ����: " + gradeScale[currentGradeIndex];
        }
    }


    public void SetGrade(string newGrade)
    {
        // gradeScale �迭���� newGrade�� ��ġ�ϴ� ������ �ε����� ã���ϴ�.
        int newIndex = System.Array.IndexOf(gradeScale, newGrade);

        // ��ȿ�� ������ ã�Ҵٸ�
        if (newIndex != -1)
        {
            currentGradeIndex = newIndex;
            UpdateGradeUI();
        }
        else
        {
            Debug.LogWarning("SetGrade: '" + newGrade + "'�� ��ȿ�� ������ �ƴմϴ�.");
        }
    }

    //�⼮����
    
    public void OnAttendanceButtonClicked()
    {
        Debug.Log("�⼮ ��ư�� Ŭ���Ǿ����ϴ�.");

        // ���� ������ �ְ� ����(�迭�� ������ ���)�� �ƴ϶�� �ε����� 1 ������ŵ�ϴ�.
        if (currentGradeIndex < gradeScale.Length - 1)
        {
            currentGradeIndex++;
        }

        Debug.Log("������ ����߽��ϴ�. ���� ����: " + gradeScale[currentGradeIndex]);
        UpdateGradeUI();
    }

 

    /// <summary>
    /// '�����' ��ư�� Ŭ������ �� ȣ��� �Լ��Դϴ�.
    /// </summary>
    public void OnRestartButtonClicked()
    {
        Debug.Log("����� ��ư�� Ŭ���Ǿ����ϴ�. ���� ���� �ٽ� �ε��մϴ�.");
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

