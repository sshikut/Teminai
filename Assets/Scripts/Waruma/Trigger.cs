using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private Vector3 lastPlayerPos;    // 마지막 위치 저장
    private bool playerInside = false;

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

            // 조금이라도 움직였는지 체크
            if (Vector3.Distance(currentPos, lastPlayerPos) > 0.01f)  // 0.01은 민감도
            {
                Debug.Log("플레이어가 트리거 존에서 움직였습니다!");
                lastPlayerPos = currentPos;
            }
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