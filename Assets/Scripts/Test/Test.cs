using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private int count = 0; // 증가할 변수

    // 버튼에 연결할 메서드
    public void OnButtonClick()
    {
        count++;
        Debug.Log("현재 값: " + count);
    }
}
