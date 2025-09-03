using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
using UnityEngine.SceneManagement; 

public class PhoneController : MonoBehaviour
{
    // Unity 인스펙터 창에서 연결할 휴대폰 UI 패널 GameObject
    public GameObject phoneUiPanel;

    // 학점을 표시할 TextMeshPro UI 요소
    public TextMeshProUGUI gradeDisplayText;

    // '+' 학점을 포함한 전체 학점 체계를 문자열 배열로 정의합니다.
    private readonly string[] gradeScale = { "F", "D", "D+", "C", "C+", "B", "B+", "A", "A+" };

    // 배열에 해당되는 번호
    private int currentGradeIndex = 0;

    void Start()
    {
        // 게임이 시작될 때 휴대폰 UI가 보이지 않도록 비활성화합니다.
        if (phoneUiPanel != null)
        {
            phoneUiPanel.SetActive(false);
        }

        // 학점 표시 UI를 업데이트합니다.
        UpdateGradeUI();
    }

    void Update()
    {
        // 매 프레임마다 'Tab' 키 입력이 있는지 확인합니다.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 키가 눌렸다면 UI 토글 함수를 호출합니다.
            TogglePhoneUI();
        }
    }

    public void TogglePhoneUI()
    {
        if (phoneUiPanel != null)
        {
            // 현재 활성화 상태를 가져와서 그 반대 값으로 설정합니다.
            bool isActive = phoneUiPanel.activeSelf;
            phoneUiPanel.SetActive(!isActive);

            // UI가 켜질 때마다 학점 정보를 최신 상태로 업데이트합니다.
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
            // 현재 인덱스에 해당하는 학점 문자열을 배열에서 찾아와 UI에 표시합니다.
            gradeDisplayText.text = "현재 학점: " + gradeScale[currentGradeIndex];
        }
    }


    public void SetGrade(string newGrade)
    {
        // gradeScale 배열에서 newGrade와 일치하는 학점의 인덱스를 찾습니다.
        int newIndex = System.Array.IndexOf(gradeScale, newGrade);

        // 유효한 학점을 찾았다면
        if (newIndex != -1)
        {
            currentGradeIndex = newIndex;
            UpdateGradeUI();
        }
        else
        {
            Debug.LogWarning("SetGrade: '" + newGrade + "'는 유효한 학점이 아닙니다.");
        }
    }

    //출석관련
    
    public void OnAttendanceButtonClicked()
    {
        Debug.Log("출석 버튼이 클릭되었습니다.");

        // 현재 학점이 최고 학점(배열의 마지막 요소)이 아니라면 인덱스를 1 증가시킵니다.
        if (currentGradeIndex < gradeScale.Length - 1)
        {
            currentGradeIndex++;
        }

        Debug.Log("학점이 상승했습니다. 현재 학점: " + gradeScale[currentGradeIndex]);
        UpdateGradeUI();
    }

 

    /// <summary>
    /// '재시작' 버튼을 클릭했을 때 호출될 함수입니다.
    /// </summary>
    public void OnRestartButtonClicked()
    {
        Debug.Log("재시작 버튼이 클릭되었습니다. 현재 씬을 다시 로드합니다.");
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

