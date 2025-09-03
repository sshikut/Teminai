using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    [Header("Anomaly Variable")]
    public bool isAnomaly = false;
    public int anomalyRate = 5;

    [Header("Anomaly List")]
    public int maxAnomalies = 5;
    public int[] anomalyArray;
    public int remainAnomaly;

    private void Start()
    {
        anomalyArray = new int[maxAnomalies];
        remainAnomaly = maxAnomalies;

        Anomaly();
    }

    public void Anomaly()
    {
        int spawnRate = Random.Range(1, 11);

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

            switch (anomalyIndex)
            {
                case 0:
                    Debug.Log("�̻����� 1");
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
            }
        }
        else
        {
            // Default �������� �ǵ�����
        }
    }
}
