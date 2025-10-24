using UnityEngine;
using UnityEngine.AI;

public class SnowMan : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        BGMManager.instance.Play(2);
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }
}