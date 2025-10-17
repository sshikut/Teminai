using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class YgerProliferation : MonoBehaviour, IInteractable
{
    [Header("생성할 프리팹 정보")]
    public GameObject originalPrefab;
    public int count = 10;

    [Header("위치 정보")]
    public List<Transform> spawnPoints;
    public Transform targetPoint;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private List<NavMeshAgent> spawnedAgents = new List<NavMeshAgent>();
    private bool prefabsSpawned = false;

    
    public bool anomalytrigger = false;

    void Start()
    {
        if (originalPrefab == null || spawnPoints == null || spawnPoints.Count < 2 || targetPoint == null)
        {
           
            enabled = false;
            return;
        }
        if (originalPrefab.GetComponent<NavMeshAgent>() == null)
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (prefabsSpawned)
        {
            MoveSpawnedObjects();
        }
    }

    public void Interact()
    {
        
        if (anomalytrigger == true)
        {
            if (prefabsSpawned) return;
            SpawnPrefabs();
        }
        
        
    }
    public void activebool()
    {
        anomalytrigger = true;
    }
    public void SpawnPrefabs()
    {
        if (prefabsSpawned) return;

        int firstHalf = count / 2;
        int secondHalf = count - firstHalf;

        SpawnAtPoint(spawnPoints[0], firstHalf);
        SpawnAtPoint(spawnPoints[1], secondHalf);

        prefabsSpawned = true;

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void SpawnAtPoint(Transform spawnPoint, int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPoint.position, out hit, 5.0f, NavMesh.AllAreas))
            {
                GameObject instance = Instantiate(originalPrefab, hit.position, spawnPoint.rotation);
                spawnedObjects.Add(instance);

                NavMeshAgent agent = instance.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    agent.updateRotation = true;
                    spawnedAgents.Add(agent);
                }
            }
            else
            {
                Debug.LogWarning($"{spawnPoint.name} NavMesh 위치");
            }
        }
    }

    private void MoveSpawnedObjects()
    {
        Vector3 targetPosition = targetPoint.position;
        foreach (NavMeshAgent agent in spawnedAgents)
        {
            if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
            {
                if (agent.destination != targetPosition)
                {
                    agent.SetDestination(targetPosition);
                }
            }
        }
    }

    public void ResetYger()
    {
        Debug.Log("프리팹 초기화");
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        this.anomalytrigger = false;
        spawnedObjects.Clear();
        spawnedAgents.Clear();
        prefabsSpawned = false;
    }
}