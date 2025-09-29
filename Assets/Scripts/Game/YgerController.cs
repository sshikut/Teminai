using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YgerController : MonoBehaviour
{
    public GameObject GoalTrigger;
    [SerializeField] private Animator anim;
    private GameObject playerObject;
    private Vector3 playerPosition;
    private Vector3 originalPosition;
    public Transform destinationTransform;
    [SerializeField] private float moveSpeed = 3.0f;
    
    public AnomalyManager anomaly;
    public TogglePhone TogglePhone;

    public GameObject yger;
    private bool isChasing = false;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerPosition = playerObject.transform.position;
        }

        originalPosition = transform.position;
        TogglePhone = FindObjectOfType<TogglePhone>();
        anomaly = FindObjectOfType<AnomalyManager>();
       
    }

   
    void Update()
    {
        
        if (isChasing)
        {
            if (playerObject != null)
            {
                
                Vector3 targetPosition = playerObject.transform.position;

                Vector3 currentPosition = transform.position;
                
                Vector3 direction = (targetPosition - currentPosition).normalized;
               
                transform.position += direction * moveSpeed * Time.deltaTime;
               
                Vector3 lookAtTarget = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                transform.LookAt(lookAtTarget);
            }
        }
    }

   
    public void YgerDash()    // 이상현상 - 10 시작
    {
        yger.SetActive(true);
        anim.Play("run"); 
        isChasing = true; 
      
    }

   
    public void ResetToOriginalPosition() // 와이거 초기상태로 초기화하는 코드
    {
        isChasing = false; 
        yger.SetActive(false);
        GoalTrigger.SetActive(false);
        transform.position = originalPosition;
        
        
    }

    public void MoveToPosition() // 와이거 위치 초기화
    {
        if (destinationTransform != null)
        {
            transform.position = destinationTransform.position;
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("붙음?");
            isChasing = false;
            //TogglePhone.DisableToggleAfterOneUse();     //폰 ui 켜고
            InteractionManager.Instance.StartFadeOut(); //페이드 아웃
            
            MoveToPosition(); //위치 초기화
            anomaly.absentCount++; // 결석 +1
            anomaly.Anomaly(); // 모든 이상현상 초기화
           
          
        }
    }
}