using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;
using UnityEngine.UI; 

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
    

   
    private List<Quiz> currentQuizzes;
    private int currentQuizIndex = 0;
    private int correctCount = 0;
    private int wrongCount = 0;

    
    public AnomalyManager anomaly;
    public GameObject QuizUI;
    void Start()
    {              
        StartGame();
    }

    public void StartGame()
    {
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
       
        quizPanel.SetActive(false);
   

        Debug.Log($"퀴즈 종료 정답: {correctCount}개, 오답: {wrongCount}개");

        
        scoreText.text = $"결과: {correctCount} / {currentQuizzes.Count}";

      
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
        QuizUI.SetActive(false);
        anomaly.Anomaly();
        Debug.Log("성공");
    }

    
    private void FailFunction()
    {
        if (anomaly != null) anomaly.absentCount++;
        InteractionManager.Instance.StartFadeOut();
        QuizUI.SetActive(false);
        anomaly.Anomaly();
        Debug.Log("실패");
    }
}