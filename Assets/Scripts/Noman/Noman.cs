using UnityEngine;

public class SimpleChase : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float rotateSpeed = 10f;

    [Tooltip("모델의 앞 방향이 Z축이 아닐 경우 보정 각도 (예: 90, 180 등)")]
    public float rotationOffsetY = 0f;

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