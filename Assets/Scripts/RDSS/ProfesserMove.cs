using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProfesserMove : MonoBehaviour
{
    public Transform professor;
    public Transform target;
    public Transform start;

    public float duration = 90f;

    public void StartMove()
    {
        StartCoroutine(MoveToPosition(target.position, duration));
    }

    public IEnumerator MoveToPosition(Vector3 targetPosition, float time)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = professor.position;

        while (elapsedTime < time)
        {
            professor.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / time);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        professor.position = targetPosition;
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
        StopAllCoroutines();
        professor.position = start.position;
    }
}
