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

    private List<NavMeshAgent> spawnedAgents = new List<NavMeshAgent>();
    private bool prefabsSpawned = false;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    
    void Start()
    {
       
        if (originalPrefab == null || spawnPoints == null || spawnPoints.Count == 0 || targetPoint == null)
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

   
  
    public void SpawnPrefabs()
    {
        if (prefabsSpawned) return;

        for (int i = 0; i < count; i++)
        {
           
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

          
            Vector3 spawnPosition = randomSpawnPoint.position;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(spawnPosition, out hit, 5.0f, NavMesh.AllAreas))
            {
               
                GameObject instance = Instantiate(originalPrefab, hit.position, randomSpawnPoint.rotation);
                NavMeshAgent agent = instance.GetComponent<NavMeshAgent>();
                spawnedObjects.Add(instance);
                if (agent != null)
                {
                    spawnedAgents.Add(agent);
                }
            }
            else
            {
                Debug.LogWarning($"{randomSpawnPoint.name}  NavMesh 위치를 찾을 수 없습니다.");
            }
        }

        prefabsSpawned = true;
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

    public void Interact()
    {
        SpawnPrefabs();
        MoveSpawnedObjects();
        
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
          
          
            audioSource.Play();
            
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
        audioSource.Stop();
        
        spawnedObjects.Clear();
        spawnedAgents.Clear();
        

        
        prefabsSpawned = false;
    }
}