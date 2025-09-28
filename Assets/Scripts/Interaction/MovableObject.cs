using UnityEngine;
using System.Collections;

public class MovableObject : MonoBehaviour, IInteractable
{
    public GameObject screen;

    public float duration = 5f;

    public Vector3 originalPosition;
    public Vector3 targetPosition;

    private Coroutine moveCoroutine;
    public bool hasMoved = false;

    public void Interact() // 인터페이스 함수
    {
        if (!hasMoved)
        {
            StartMovement();
            hasMoved = true;
        }
        else
        {
            ResetPosition();
            hasMoved = false;
        }
    }

    public void StartMovement()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveTo(targetPosition));
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

        float elapsedTime = 0f; 

        while (elapsedTime < duration)
        {
            screen.transform.position = Vector3.Lerp(screen.transform.position, target, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        screen.transform.position = target;
    }

    public void DownPosition()
    {
        screen.transform.position = targetPosition;
        hasMoved = true;
    }
}