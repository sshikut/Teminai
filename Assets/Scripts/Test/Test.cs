using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private int count = 0; // ������ ����

    // ��ư�� ������ �޼���
    public void OnButtonClick()
    {
        count++;
        Debug.Log("���� ��: " + count);
    }
}
