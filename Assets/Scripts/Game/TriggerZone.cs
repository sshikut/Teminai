using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [Tooltip("트리거를 발동시킬 오브젝트의 태그를 입력하세요.")]
    public string targetTag = "Player";
    public UnityEvent onTriggerEnter;
    
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(targetTag))
        {
            onTriggerEnter.Invoke(); // 트리거 함수
          
            gameObject.SetActive(false); // 트리거 감추기
        }
    }
}