using UnityEngine;
using UnityEngine.AI;

public class SnowMan : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        BGMManager.instance.Play(2);
    }

    void OnDisable()
    {
        BGMManager.instance.Play(0);
    }

    void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }
}