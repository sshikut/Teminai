using UnityEngine;
using System.Collections;

public class MovableObject : MonoBehaviour, IInteractable
{
   
    public float targetYPosition = 0f;
    public float moveSpeed = 2f;
    private Vector3 originalPosition;
    private Coroutine moveCoroutine;
    public bool hasMoved = false;

   
    private void Awake()
    {
        originalPosition = transform.position;
    }

    public void Interact() // 인터페이스 함수
    {
        // 정확하게 이 스크립트가 어떤 역할을 하는지 몰라서 일단 이거만 해둠 
        ResetPosition();
    }

    public void StartMovement()
    {
       
        if (hasMoved) return;

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        gameObject.SetActive(true);
       
        moveCoroutine = StartCoroutine(MoveTo(new Vector3(originalPosition.x, targetYPosition, originalPosition.z)));
    }

    
    public void ResetPosition()
    {
        
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

      
        moveCoroutine = StartCoroutine(MoveTo(originalPosition));
       
    }

    
    private IEnumerator MoveTo(Vector3 target)
    {
        
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
           
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null; 
        }

      
        transform.position = target;

       
        if (transform.position.y == targetYPosition)
        {
            hasMoved = true;
        }
        else
        {
            hasMoved = false;
        }
    }
}