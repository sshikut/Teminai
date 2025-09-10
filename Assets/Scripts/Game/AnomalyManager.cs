using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    [Header("Game Settings")]
    public AnomalyMethod anomaly;
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

    private void Start()
    {
        anomalyArray = new int[maxAnomalies];
        remainAnomaly = maxAnomalies;

        Anomaly();
    }

    public void Anomaly()
    {
        anomaly.InitAnomaly(); // �̻����� �ʱ�ȭ

        if (loopCount >= clearCount)
        {
            Clear();
        }

        if (absentCount >= 3) // �Ἦ �� 3 �̻�Ǹ� �ʱ�ȭ
        {
            loopCount = 0;
            absentCount = 0;
            InitAnomaly();
        }

        int spawnRate = Random.Range(1, 11);

        if (loopCount < 3) anomalyRate = 3;
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
    }

    // isAnomaly
    // bool ���� true = �̻�����
    // bool ���� false = ����

    void PlayAnomaly(bool isAnomaly)
    {
        if (isAnomaly)
        {
            int anomalyIndex;

            while (true)
            {
                anomalyIndex = Random.Range(0, anomalyArray.Length);

                if (anomalyArray[anomalyIndex] == 0)
                {
                    break;
                }
            }

            remainAnomaly--;
            anomalyArray[anomalyIndex]++;

            switch (anomalyIndex)
            {
                case 0:
                    Debug.Log("�̻����� 1 : ��â��");
                    anomaly.Anomaly_1();
                    break;

                case 1:
                    Debug.Log("�̻����� 2");
                    break;

                case 2:
                    Debug.Log("�̻����� 3");
                    break;

                case 3:
                    Debug.Log("�̻����� 4");
                    break;

                case 4:
                    Debug.Log("�̻����� 5");
                    break;

                case 5:
                    Debug.Log("�̻����� 6");
                    break;

                case 6:
                    Debug.Log("�̻����� 7");
                    break;

                case 7:
                    Debug.Log("�̻����� 8");
                    break;

                case 8:
                    Debug.Log("�̻����� 9");
                    break;

                case 9:
                    Debug.Log("�̻����� 10");
                    break;

                case 10:
                    Debug.Log("�̻����� 11");
                    break;

                case 11:
                    Debug.Log("�̻����� 12");
                    break;

                case 12:
                    Debug.Log("�̻����� 13");
                    break;

                case 13:
                    Debug.Log("�̻����� 14");
                    break;

                case 14:
                    Debug.Log("�̻����� 15");
                    break;

                case 15:
                    Debug.Log("�̻����� 16");
                    break;

                case 16:
                    Debug.Log("�̻����� 17");
                    break;

                case 17:
                    Debug.Log("�̻����� 18");
                    break;

                case 18:
                    Debug.Log("�̻����� 19");
                    break;

                case 19:
                    Debug.Log("�̻����� 20");
                    break;
            }
        }
        else
        {
            // Default �������� �ǵ�����
        }
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
