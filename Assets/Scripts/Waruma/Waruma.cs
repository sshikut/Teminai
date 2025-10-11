using UnityEngine;
using System.Collections;

public class Waruma : MonoBehaviour
{
    public Animator anim;
    public Collider absentCol;
    public bool isLooping = false;
    public int playMusicTrack = 1;

    [Header("대기 시간 설정")]
    [Tooltip("Back 상태에서 대기하는 시간(초)")]
    public float backWaitTime = 3f;

    [Tooltip("Front 상태에서 대기하는 시간(초)")]
    public float frontWaitTime = 3f;

    private Coroutine loopCo;

    void OnEnable()
    {
        if (BGMManager.instance != null)
        {
            BGMManager.instance.Play(playMusicTrack);
            BGMManager.instance.FadeInMusic();
        }
        else
        {
            Debug.LogWarning("[Waruma] BGMManager.instance == null，음악 재생을 건너뜁니다.");
        }

        // 재활성화 시 루프 재시작을 위해 리셋
        isLooping = false;

        // 혹시 남아있던 코루틴이 있으면 정리 후 시작
        if (loopCo != null) { StopCoroutine(loopCo); loopCo = null; }
        loopCo = StartCoroutine(AnimationLoop());
    }

    void OnDisable()
    {
        // 코루틴 안전 종료 및 플래그 리셋
        if (loopCo != null)
        {
            StopCoroutine(loopCo);
            loopCo = null;
        }
        isLooping = false;

        if (absentCol != null) absentCol.enabled = false;

        if (BGMManager.instance != null)
        {
            BGMManager.instance.Play(0);
        }
    }

    void Update()
    {
        if (anim == null || absentCol == null) return;

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
        if (isLooping || anim == null) yield break;
        isLooping = true;

        while (isActiveAndEnabled)
        {
            // Turn → Back
            anim.Play("Turn", 0, 0f); // 레이어 0，정규화시간 0으로 즉시 재생
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Back"));
            yield return new WaitForSeconds(backWaitTime);

            // TurnForDeath → Front
            anim.Play("TurnForDeath", 0, 0f);
            yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Front"));
            yield return new WaitForSeconds(frontWaitTime);
        }

        isLooping = false; // 루프가 종료되면 플래그 정리
    }
}