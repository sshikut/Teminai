using System;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public static event Action OnAnomalyHappened;

    [Header("Game Settings")]
    public AnomalyMethod anomaly;
    public RDSS rdss;
    public int loopCount = 0;
    public int absentCount = 0;
    public int clearCount = 8;
    public GameObject clearImage;

    [Header("Anomaly Variable")]
    public bool isAnomaly = false;
    public int anomalyRate = 5;

    [Header("Anomaly List")]
    public int maxAnomalies = 20;
    public int[] anomalyArray;
    public int remainAnomaly;

    [Header("Anomaly Objects")]
    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;

    public TriggerZone triggerZone;
    public MovableObject movableObject;
    public TimerManager timerManager;
    public YgerController ygerController;

    [Header("Trigger-Once Index Range")]
    [Tooltip("예: 9~13번을 1회용으로 하려면 8 ~ 12 입력")]
    public int minNum;
    public int maxNum;



    private void Start()
    {
        anomalyArray = new int[maxAnomalies];
        remainAnomaly = maxAnomalies;
        triggerZone = FindObjectOfType<TriggerZone>();
        movableObject = FindObjectOfType<MovableObject>();
        timerManager = FindObjectOfType<TimerManager>();
        ygerController = FindObjectOfType<YgerController>();
        Anomaly();
    }

    public void Anomaly()
    {
        ygerController.ResetToOriginalPosition();
        rdss.RandomSituation();
        anomaly.InitAnomaly(); // 이상현상 초기화
        timerManager.timerText.text = null;

        if (loopCount >= clearCount)
        {
            Clear();
        }

        if (absentCount >= 3) // 결석 수 3 이상되면 초기화
        {
            loopCount = 0;
            absentCount = 0;
            InitAnomaly();
        }

        int spawnRate = UnityEngine.Random.Range(1, 11);

        if (loopCount < 2) anomalyRate = 3;
        else if (loopCount < 3) anomalyRate = 4;
        else if (loopCount < 6) anomalyRate = 5;
        else anomalyRate = 7;

        if (spawnRate <= anomalyRate)
        {
            isAnomaly = true;

        }
        else
        {
            isAnomaly = false;
        }

        PlayAnomaly(isAnomaly);
        OnAnomalyHappened?.Invoke();
    }

    // isAnomaly
    // bool 값이 true = 이상현상
    // bool 값이 false = 정상

    void PlayAnomaly(bool isAnomaly)
    {
        // 1) 이번 라운드가 '정상'이면 트리거를 다회용으로 풀고 종료
        if (!isAnomaly)
        {
            triggerZone.triggerOnce = false;
            return;
        }

        // 2) 아직 안 나온 인덱스 하나 찾기 (기존 코드 참조)
        int anomalyIndex = -1;
        int start = UnityEngine.Random.Range(0, anomalyArray.Length);
        for (int k = 0; k < anomalyArray.Length; k++)
        {
            int idx = (start + k) % anomalyArray.Length;
            if (anomalyArray[idx] == 0) // 0 = 아직 안 나온 번호
            {
                anomalyIndex = idx;
                break;
            }
        }

        // 못 찾으면(= 전부 사용됨) 조용히 종료
        if (anomalyIndex < 0) return;

        // 3) 선택된 번호를 사용 처리(재등장 방지)
        anomalyArray[anomalyIndex] = 1;

        // 4) 특수 케이스: 9~13번은 트리거를 '1회용'으로만 전환
        if (anomalyIndex >= minNum && anomalyIndex <= maxNum)
        {
            Debug.Log($"이상현상 {anomalyIndex + 1} : Trigger Once");
            triggerZone.triggerOnce = true;
            return;
        }

        // 5) 일반 케이스: 인덱스로 실행 위임
        Debug.Log($"이상현상 {anomalyIndex + 1} 실행");
        anomaly.TriggerAnomaly(anomalyIndex);
    }

    void InitAnomaly()
    {
        for (int i = 0; i < anomalyArray.Length; i++)
        {
            anomalyArray[i] = 0;
        }

        remainAnomaly = maxAnomalies;
    }


    void Clear()
    {
        // clearImage.SetActive(true);
    }
}
