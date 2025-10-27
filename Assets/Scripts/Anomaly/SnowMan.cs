using UnityEngine;
using UnityEngine.AI;

public class SnowMan : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    public float viewAngle = 45f; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!player) return;

        // 플레이어가 바라보는 방향과 눈사람 방향의 각도 계산
        Vector3 toSnowman = (transform.position - player.position).normalized;
        float angle = Vector3.Angle(player.forward, toSnowman);

        // 각도가 작으면 보고 있는 거니까 멈추고, 아니면 추격
        if (angle < viewAngle)
        {
            agent.isStopped = true; // 멈춤
        }
        else
        {
            agent.isStopped = false; // 이동
            agent.SetDestination(player.position);
        }
    }
}