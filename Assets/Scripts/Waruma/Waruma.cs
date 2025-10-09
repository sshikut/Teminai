using UnityEngine;
using System.Collections;

public class Waruma : MonoBehaviour
{
    public Animator anim;
    public Collider absentCol;
    public bool isLooping = false;

    [Header("대기 시간 설정")]
    [Tooltip("Back 상태에서 대기하는 시간(초)")]
    public float backWaitTime = 3f;

    [Tooltip("Front 상태에서 대기하는 시간(초)")]
    public float frontWaitTime = 3f;

    void Start()
    {
        StartCoroutine(AnimationLoop());
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Front"))
        {
            absentCol.enabled = true;   // 앞 봄 → 패널티 가능
        }
        else if (stateInfo.IsName("Back"))
        {
            absentCol.enabled = false;  // 뒤 봄 → 패널티 없음
        }
    }

    IEnumerator AnimationLoop()
    {
        if (isLooping) yield break;
        isLooping = true;

        while (true)
        {
            // Turn → Back
            anim.Play("Turn");
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Back"));
            yield return new WaitForSeconds(backWaitTime);

            // TurnForDeath → Front
            anim.Play("TurnForDeath");
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Front"));
            yield return new WaitForSeconds(frontWaitTime);
        }
    }
}