using UnityEngine;

public class FootstepController : MonoBehaviour
{
    [Header("추적할 오브젝트")]
    public GameObject target;

    public CharacterController controller;

    [Header("AudioManager 사운드 이름")]
    public string footstepSoundName = "Footstep";
   
    [Header("이동 판정 / 간격")]
    public float moveThreshold = 0.01f; // 프레임당 거리 기준
    public float stepDistance = 1.5f;   // 누적 이동거리마다 발소리
    public float speedThreshold = 0.075f; // ★ m/s 기준(아주 작게만 움직여도 잡히게)

    [Header("최소 간격(쿨타임)")]
    public float CoolTime = 0.5f;

    private Vector3 lastPos;
    private float accumulatedDistance;
    private bool wasMoving;
    private float nextStepTime;

    void Start()
    {
        if (controller == null)
        {
            controller = target.GetComponent<CharacterController>();
        }
        if (target == null) target = gameObject;
        lastPos = target.transform.position;
        accumulatedDistance = 0f;
        wasMoving = false;
        nextStepTime = 0f;
    }

    void Update()
    {
        if (controller == null) return;

        Vector3 currentPos = target.transform.position;
        float frameDist = Vector3.Distance(currentPos, lastPos);
        float speed = frameDist / Mathf.Max(Time.deltaTime, 1e-6f);     // ★ 추가
        bool isMoving = (frameDist > moveThreshold) || (speed > speedThreshold); // ★ 수정
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

                if (AudioManager.instance != null && controller.isGrounded == true)
                {
                    AudioManager.instance.Play(footstepSoundName);
                }
                accumulatedDistance = 0f;
                nextStepTime = now + CoolTime;
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
