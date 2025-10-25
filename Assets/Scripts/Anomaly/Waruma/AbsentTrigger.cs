using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsentTrigger : MonoBehaviour
{
    private Vector3 lastPlayerPos;    // 마지막 위치 저장
    private bool playerInside = false;
    public AnomalyManager anomaly;
    public GameObject AnomalyObject;
    [Header("1 = 움직이면 잡히기, 2 = 안 움직여도 잡히기")]
    [SerializeField] private int mode = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 트리거 존에 들어옴!");
            playerInside = true;
            lastPlayerPos = other.transform.position;

            // 모드 2면 들어온 즉시 패널티 발생
            if (mode == 2)
            {
                Debug.Log("플레이어가 안 움직여도 잡히는 모드, 즉시 패널티 발생!");
                TriggerPenalty();
                playerInside = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerInside && other.CompareTag("Player"))
        {
            Vector3 currentPos = other.transform.position;

            // 모드 1: 움직임 감지
            if (mode == 1 && Vector3.Distance(currentPos, lastPlayerPos) > 0.01f)
            {
                Debug.Log("플레이어가 움직여서 패널티 발생!");
                TriggerPenalty();
                playerInside = false;
            }

            lastPlayerPos = currentPos;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 트리거 존에서 나감!");
            playerInside = false;
        }
    }

    private void TriggerPenalty()
    {
        anomaly.absentCount++;
        
        InteractionManager.Instance.StartFadeOut(() =>
        {
            anomaly.Anomaly(); // 검정이 한 프레임 실제로 그려진 뒤 실행
        });
    }

   
    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += resetObject;
    }

    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= resetObject;
    }
    public void resetObject()
    {
        AnomalyObject.SetActive(false);
    }
}
