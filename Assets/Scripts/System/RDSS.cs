using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Situation
{
    public string description;
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;
}

public class RDSS : MonoBehaviour
{
    public Situation[] situations;
    public int currentSituationIndex = -1;

    // RDS_0 = 교수는 강의 중
    // RDS_1 = 교수는 쉬는 중
    // RDS_2 = 교수는 강의하러 오는 중
    // RDS_3 = 임시
    // RDS_4 = 임시

    public void RandomSituation()
    {
        if (currentSituationIndex != -1)
        {
            situations[currentSituationIndex].onDeactivate.Invoke();
        }

        int randomIndex = Random.Range(0, situations.Length);
        currentSituationIndex = randomIndex;

        situations[currentSituationIndex].onActivate.Invoke();
    }

    public void SelectSituation(int index)
    {
        if (index < 0 || index >= situations.Length)
        {
            return;
        }

        if (currentSituationIndex != -1)
        {
            situations[currentSituationIndex].onDeactivate.Invoke();
        }

        currentSituationIndex = index;

        situations[currentSituationIndex].onActivate.Invoke();
    }

    public void InitAllSituations()
    {
        for (int i = 0; i < situations.Length; i++)
        {
            situations[i].onDeactivate.Invoke();
        }
        currentSituationIndex = -1;
    }
}
