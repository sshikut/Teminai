using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProfesserMove : MonoBehaviour
{
    public Transform professor;
    public Transform target;
    public Transform secTarget;
    public Transform finalTarget;
    public Transform start;
    public Transform finalRotation;
    private Coroutine moveCoroutine;

    public float duration = 90f;
    public float secDuration = 5f;
    public float finalDuration = 5f;

    public GameObject arriveProfessor;
    public DoorControl door;

    public void StartMove()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveSequence());
    }

    public IEnumerator MoveSequence()
    {
        // --- 1단계: 첫 번째 target으로 이동 ---
        yield return StartCoroutine(MoveToTarget(target.position, duration));

        // --- 2단계: 두 번째 secTarget으로 이동 및 회전 ---
        yield return StartCoroutine(MoveAndRotateToTarget(secTarget.position, secTarget.rotation, secDuration));

        // --- 3단계: 마지막 finalTarget으로 이동 및 회전 ---
        yield return StartCoroutine(MoveAndRotateToTarget(finalTarget.position, finalTarget.rotation, finalDuration));

        // --- 4단계: 최종 회전 ---
        yield return StartCoroutine(RotateToTarget(finalRotation.rotation, 1f)); // 1초 동안 최종 회전

        Debug.Log("모든 시퀀스 완료!");
        arriveProfessor.SetActive(true);
        professor.gameObject.SetActive(false);
        moveCoroutine = null;
    }

    // 지정된 위치로 '이동'만 하는 코루틴
    private IEnumerator MoveToTarget(Vector3 targetPosition, float time)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = professor.position;

        while (elapsedTime < time)
        {
            professor.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        professor.position = targetPosition; // 오차 보정

        if (!door.hasMoved)
        {
            door.Door();
        }
    }

    // 지정된 위치와 회전으로 '이동 및 회전'을 동시에 하는 코루틴
    private IEnumerator MoveAndRotateToTarget(Vector3 targetPosition, Quaternion targetRotation, float time)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = professor.position;
        Quaternion startingRotation = professor.rotation;

        while (elapsedTime < 0.5f)
        {
            professor.rotation = Quaternion.Slerp(startingRotation, targetRotation, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        professor.rotation = targetRotation;

        elapsedTime = 0f;

        while (elapsedTime < time)
        {
            professor.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        professor.position = targetPosition;
    }

    // 지정된 회전으로 '회전'만 하는 코루틴
    private IEnumerator RotateToTarget(Quaternion targetRotation, float time)
    {
        float elapsedTime = 0f;
        Quaternion startingRotation = professor.rotation;

        while (elapsedTime < time)
        {
            professor.rotation = Quaternion.Slerp(startingRotation, targetRotation, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        professor.rotation = targetRotation;
    }

    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += ResetPosition;
    }


    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= ResetPosition;
    }

    public void ResetPosition()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        professor.position = start.position;
        professor.rotation = start.rotation;
        arriveProfessor.SetActive(false);
        professor.gameObject.SetActive(true);
    }
}
