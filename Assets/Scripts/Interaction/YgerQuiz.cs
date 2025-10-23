using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // --- 추가된 부분 ---

public class YgerQuiz : MonoBehaviour
{
    [System.Serializable]
    public class Quiz
    {
        public string question;
        public List<string> options;
        public int correctAnswerIndex;
    }

    [Header("퀴즈 데이터")]
    public List<Quiz> allQuizzes;

    [Header("게임 설정")]
    public int quizzesToPlay = 5;
    public int correctAnswersForSuccess = 3;

    [Header("연결된 UI 요소")]
    public GameObject quizPanel;
    public TextMeshProUGUI questionText;
    public List<Button> optionButtons;
    public TextMeshProUGUI scoreText;

    // --- 추가된 부분 (레이캐스트용) ---
    [Header("레이캐스트 설정")]
    public GraphicRaycaster uiRaycaster; // 퀴즈 UI가 있는 Canvas의 GraphicRaycaster
    public EventSystem eventSystem;     // 씬의 EventSystem
    private Camera mainCamera;
    // --- ---

    private List<Quiz> currentQuizzes;
    private int currentQuizIndex = 0;
    private int correctCount = 0;
    private int wrongCount = 0;

    public AnomalyManager anomaly;
    public GameObject QuizUI;

     public GameObject DoorObject;
    void Start()
    {


        // --- 수정된 부분 (레이캐스트 설정 초기화) ---
        mainCamera = Camera.main;

        if (eventSystem == null)
        {
            eventSystem = EventSystem.current; // 씬에서 현재 EventSystem 찾기
        }

        if (uiRaycaster == null && quizPanel != null)
        {
            // quizPanel이 속한 Canvas에서 GraphicRaycaster 찾기
            uiRaycaster = quizPanel.GetComponentInParent<Canvas>().GetComponent<GraphicRaycaster>();
        }

        if (uiRaycaster == null)
        {
            Debug.LogError("GraphicRaycaster가 없습니다! 퀴즈 UI가 있는 Canvas에 GraphicRaycaster 컴포넌트를 추가해주세요.");
        }
        // --- ---

        StartGame();
    }

    // --- 추가된 부분 (Update 메서드) ---
    void Update()
    {
        // 퀴즈 패널이 비활성화 상태면 레이캐스트를 실행하지 않음
        if (!quizPanel.activeSelf)
        {
            return;
        }

        // 'Fire1' 입력 (기본: 마우스 좌클릭, 왼쪽 Ctrl)을 감지
        if (Input.GetButtonDown("Fire1"))
        {
            CheckForUIClick();
        }
    }

    // --- 추가된 부분 (UI 클릭 체크 함수) ---
    private void CheckForUIClick()
    {
        if (uiRaycaster == null || eventSystem == null) return;


        PointerEventData pointerData = new PointerEventData(eventSystem);

        pointerData.position = new Vector2(Screen.width / 2f, Screen.height / 2f);


        List<RaycastResult> results = new List<RaycastResult>();

        uiRaycaster.Raycast(pointerData, results);


        if (results.Count > 0)
        {

            GameObject hitObject = results[0].gameObject;


            Button hitButton = hitObject.GetComponentInParent<Button>();


            if (hitButton != null && optionButtons.Contains(hitButton))
            {
                Debug.Log("레이캐스트로 버튼 클릭: " + hitButton.name);


                hitButton.onClick.Invoke();
            }
        }
    }
    // --- ---

    public void StartGame()
    {
        InteractionManager.Instance.StartFadeOut();

            int layerIndex = LayerMask.NameToLayer("UI");
           DoorObject.layer = layerIndex;
        correctCount = 0;
        wrongCount = 0;
        currentQuizIndex = 0;

        quizPanel.SetActive(true);
        Shuffle(allQuizzes);

        currentQuizzes = allQuizzes.GetRange(0, Mathf.Min(quizzesToPlay, allQuizzes.Count));

        if (currentQuizzes.Count > 0)
        {
            DisplayQuiz(currentQuizzes[currentQuizIndex]);
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    private void DisplayQuiz(Quiz quiz)
    {
        questionText.text = quiz.question;
        scoreText.text = $" {correctCount} / {currentQuizzes.Count}";
        for (int i = 0; i < optionButtons.Count; i++)
        {
            if (i < quiz.options.Count)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = quiz.options[i];

                optionButtons[i].onClick.RemoveAllListeners();
                int optionIndex = i;
                optionButtons[i].onClick.AddListener(() => SubmitAnswer(optionIndex));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SubmitAnswer(int selectedOptionIndex)
    {
        if (currentQuizIndex >= currentQuizzes.Count)
        {
            Debug.Log("모든 퀴즈가 종료");
            return;
        }

        Quiz currentQuiz = currentQuizzes[currentQuizIndex];

        if (selectedOptionIndex == currentQuiz.correctAnswerIndex)
        {
            correctCount++;
            Debug.Log("정답");
        }
        else
        {
            wrongCount++;
            Debug.Log($"오답. 정답은 {currentQuiz.correctAnswerIndex + 1}번");
        }

        currentQuizIndex++;

        if (currentQuizIndex < currentQuizzes.Count)
        {
            DisplayQuiz(currentQuizzes[currentQuizIndex]);
        }
        else
        {
            EvaluateResult();
        }
    }

    private void EvaluateResult()
    {
       

        Debug.Log($"퀴즈 종료 정답: {correctCount}개, 오답: {wrongCount}개");



        if (correctCount >= correctAnswersForSuccess)
        {
            SuccessFunction();
        }
        else
        {
            FailFunction();
        }
    }

    private void SuccessFunction()
    {
        if (anomaly != null) anomaly.loopCount++;
        InteractionManager.Instance.StartFadeOut();

        anomaly.Anomaly();
        
        scoreText.text = null;



        Debug.Log("성공");
    }

    private void FailFunction()
    {
        if (anomaly != null) anomaly.absentCount++;
        InteractionManager.Instance.StartFadeOut();

        anomaly.Anomaly();
        //int layerIndex = LayerMask.NameToLayer("Interactable");
        //   DoorObject.layer = layerIndex;
        scoreText.text = null;

        Debug.Log("실패");
    }
}