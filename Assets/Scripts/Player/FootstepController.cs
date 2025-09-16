using UnityEngine;

public class FootstepController : MonoBehaviour
{
    [Header("추적할 오브젝트")]
    public GameObject target;

    [Header("AudioManager 사운드 이름")]
    public string footstepSoundName = "Footstep";

    [Header("이동 판정 / 간격")]
    public float moveThreshold = 0.04f; // 움직임 감지 민감도
    public float stepDistance = 1.6f;   // 누적 이동거리마다 발소리

    [Header("최소 간격(쿨타임)")]
    public float CoolTime = 0.25f; // 발소리 최소 간격(초)

    private Vector3 lastPos;
    private float accumulatedDistance;
    private bool wasMoving;
    private float nextStepTime; // 쿨타임 만료 시각

    void Start()
    {
        if (target == null) target = gameObject;
        lastPos = target.transform.position;
        accumulatedDistance = 0f;
        wasMoving = false;
        nextStepTime = 0f;
    }

    void Update()
    {
        Vector3 currentPos = target.transform.position;
        float frameDist = Vector3.Distance(currentPos, lastPos);
        bool isMoving = frameDist > moveThreshold;
        float now = Time.time;

        if (isMoving)
        {
            accumulatedDistance += frameDist;

            if (accumulatedDistance >= stepDistance && now >= nextStepTime)
            {
                if (AudioManager.instance == null)
                {
                    Debug.LogWarning("AudioManager.instance == null");
                }

                if (AudioManager.instance != null)
                {
                    AudioManager.instance.Play(footstepSoundName);
                }
                accumulatedDistance = 0f;
                nextStepTime = now + CoolTime; // 쿨타임 시작
            }
        }
        else
        {
            accumulatedDistance = 0f;
        }

        wasMoving = isMoving;
        lastPos = currentPos;
    }
}
