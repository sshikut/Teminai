using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 들어온 오브젝트가 Player 태그인지 확인
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 트리거 존에 들어옴!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 트리거 존에서 나감!");
        }
    }
}