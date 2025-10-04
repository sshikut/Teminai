using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private Vector3 lastPlayerPos;    // 마지막 위치 저장
    private bool playerInside = false;
    public AnomalyManager anomaly;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 트리거 존에 들어옴!");
            playerInside = true;
            lastPlayerPos = other.transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerInside && other.CompareTag("Player"))
        {
            Vector3 currentPos = other.transform.position;

            // 조금이라도 움직임 감지
            if (Vector3.Distance(currentPos, lastPlayerPos) > 0.01f)
            {
                Debug.Log("플레이어가 움직여서 패널티 발생!");

                // 패널티 적용
                anomaly.absentCount++;

                InteractionManager.Instance.StartFadeOut(() =>
                {
                    anomaly.Anomaly(); // 검정이 한 프레임 실제로 그려진 뒤 실행
                });

                // 한 번만 적용하고 싶다면 여기서 playerInside를 false로
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
}