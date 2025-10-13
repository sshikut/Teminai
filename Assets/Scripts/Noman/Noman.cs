using UnityEngine;

public class SimpleChase : MonoBehaviour
{
    private Vector3 startPosition;

    public Transform player;
    public float speed = 3f;
    public float rotateSpeed = 10f;

    [Tooltip("모델의 앞 방향이 Z축이 아닐 경우 보정 각도 (예: 90, 180 등)")]
    public float rotationOffsetY = 0f;

    void Awake()
    {
        // 처음 위치를 기록
        startPosition = transform.position;
    }
    void OnEnable()
    {
        // 비활성화됐다가 다시 활성화될 때, 처음 위치로 리셋
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
    }


    void Update()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;

        transform.position += dir * speed * Time.deltaTime;

        if (dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            // 모델이 옆을 보고 있으면 Y축으로 오프셋을 줌
            targetRot *= Quaternion.Euler(0, rotationOffsetY, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }
    }
}