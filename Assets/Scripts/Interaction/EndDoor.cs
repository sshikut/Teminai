using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour, IInteractable
{
    public bool isMoving = false;
    public bool hasMoved = false;
    public float duration = 1f;
    public float targetZ = -90f;

    public void Interact()
    {
        Door();
    }

    public void Door()
    {
        if (isMoving) return;

        isMoving = true;

        if (!hasMoved)
        {
            targetZ = -90f;
            hasMoved = true;
        }
        else
        {
            targetZ = 90f;
            hasMoved = false;
        }
        StartCoroutine(RotateToTarget());
        InteractionManager.Instance.StartFadeOut(() =>
        {
            
                SceneManager.LoadScene("엔딩 연출");
            
        } );
    }
    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += InitDoor;
    }

    // 오브젝트가 비활성화될 때 구독을 해제합니다. (매우 중요!)
    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= InitDoor;
    }

    void InitDoor()
    {
        if (hasMoved)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 90f);
            hasMoved = false;
        }
    }

    IEnumerator RotateToTarget()
    {
        float elapsedTime = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, targetZ);

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;

        isMoving = false;
    }
}
