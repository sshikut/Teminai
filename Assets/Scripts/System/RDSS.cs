using UnityEngine;

public class RDSS : MonoBehaviour
{
    public GameObject[] situations;
    public int random;

    // RDS_0 = 교수는 강의 중
    // RDS_1 = 교수는 쉬는 중
    // RDS_2 = 교수는 강의하러 오는 중
    // RDS_3 = 임시
    // RDS_4 = 임시

    public void RandomSituation()
    {
        InitSituation();

        random = Random.Range(0, situations.Length);

        situations[random].SetActive(true);
    }

    public void InitSituation()
    {
        for (int i = 0; i < situations.Length; i++)
        {
            situations[i].SetActive(false);
        }
    }

    public void SelectSituation(int num)
    {
        InitSituation();
        situations[num].SetActive(true);
    }
}
