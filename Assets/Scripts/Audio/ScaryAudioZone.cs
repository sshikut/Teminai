using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ScaryAudioZone : MonoBehaviour
{
    [Header("Zone Sound")]
    public AudioSource zoneSource;         // 액자 오디오소스(없으면 자동 GetComponent)

    [Header("BGM Control")]
    [Tooltip("존에 들어오면 BGM 볼륨을 어디까지 내릴지 (0=완전무음)")]
    [Range(0f, 1f)] public float bgmDuckVolume = 0f;
    [Tooltip("BGM 페이드 시간(초)")]
    public float bgmFadeSeconds = 0.35f;
    [Tooltip("존을 나가면 Zone 소리를 멈출지 (아니면 자연감쇠에 맡김)")]
    public bool stopZoneOnExit = true;

    int playersInside = 0;

    void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void Awake()
    {
        if (!zoneSource) zoneSource = GetComponent<AudioSource>();
        var col = GetComponent<Collider>();
        if (col) col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!IsPlayer(other)) return;
        playersInside++;
        if (playersInside == 1)
            EnterZone();
    }

    void OnTriggerExit(Collider other)
    {
        if (!IsPlayer(other)) return;
        playersInside = Mathf.Max(0, playersInside - 1);
        if (playersInside == 0)
            ExitZone();
    }

    bool IsPlayer(Collider col)
    {
        // 태그 사용 안 하면 여기서 다른 조건으로 바꿔도 됨.
        return col.CompareTag("Player");
    }

    void EnterZone()
    {
        // BGM 낮추기(또는 0)
        var bgm = BGMManager.instance;
        if (bgm && bgm.source)
            StartCoroutine(FadeBGMTo(bgm, bgmDuckVolume, bgmFadeSeconds));

        // 존 사운드 재생
        if (zoneSource && !zoneSource.isPlaying)
            zoneSource.Play();
    }

    void ExitZone()
    {
        // BGM 복귀
        var bgm = BGMManager.instance;
        if (bgm && bgm.source)
            StartCoroutine(FadeBGMTo(bgm, 1f, bgmFadeSeconds));

        // 존 사운드 정지(옵션)
        if (stopZoneOnExit && zoneSource && zoneSource.isPlaying)
            zoneSource.Stop();
    }

    IEnumerator FadeBGMTo(BGMManager bgm, float target, float seconds)
    {
        var src = bgm.source;
        float start = src.volume;
        float t = 0f;

        // target이 0이면 완전히 0까지 내려간 뒤 일시정지해서 성능/믹서 절약
        bool pauseAtEnd = target <= 0.0001f;

        // 페이드 중엔 일단 꼭 재생 상태 보장
        if (pauseAtEnd == false && !src.isPlaying)
            bgm.UnPause();

        while (t < seconds)
        {
            t += Time.deltaTime;
            float k = seconds > 0f ? t / seconds : 1f;
            src.volume = Mathf.Lerp(start, target, k);
            yield return null;
        }
        src.volume = target;

        if (pauseAtEnd)
            bgm.Pause();
    }
}
