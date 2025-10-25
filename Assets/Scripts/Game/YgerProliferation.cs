using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class SpawnPointInfo
{
    public Transform spawnTransform; // 스폰될 위치

    [Header("Random Spawn Interval")]
    public float minSpawnInterval = 3f;
    public float maxSpawnInterval = 8f;

    [Header("Random Agent Speed")]
    public float minAgentSpeed = 2.5f;
    public float maxAgentSpeed = 5f;

    [HideInInspector]
    public Coroutine spawnCoroutine;
}

public class YgerProliferation : MonoBehaviour, IInteractable
{
    [Header("생성할 프리팹 정보")]
    public GameObject originalPrefab;
    public int maxSpawnCount = 10;

    [Header("위치 정보")]
    public List<SpawnPointInfo> spawnPoints;
    public Transform targetPoint;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private bool isSpawning = false;


    public bool anomalytrigger = false;
    public AudioSource ddalkak;

    public void Interact()
    {
        
        if (anomalytrigger == true)
        {
            if (isSpawning) return;
            StartSpawning();
        }

        ddalkak.Play();
    }

    public void activebool()
    {
        anomalytrigger = true;
    }

  

    public void StartSpawning()
    {
        if (isSpawning) return;
        isSpawning = true;

        // 모든 스폰 지점에 대해 개별 스폰 코루틴을 시작
        foreach (var pointInfo in spawnPoints)
        {
            pointInfo.spawnCoroutine = StartCoroutine(SpawnPeriodically(pointInfo));
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopSpawning()
    {
        if (!isSpawning) return;
        isSpawning = false;

        // 모든 스폰 코루틴을 중지
        foreach (var pointInfo in spawnPoints)
        {
            if (pointInfo.spawnCoroutine != null)
            {
                StopCoroutine(pointInfo.spawnCoroutine);
            }
        }

        // 생성된 모든 오브젝트 파괴
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedObjects.Clear();

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        anomalytrigger = false;
    }

    private IEnumerator SpawnPeriodically(SpawnPointInfo spawnInfo)
    {
        SpawnSingleAgent(spawnInfo);

        while (isSpawning)
        {
            // 1. 설정된 최소/최대 주기 사이에서 랜덤한 대기 시간을 계산
            float randomInterval = Random.Range(spawnInfo.minSpawnInterval, spawnInfo.maxSpawnInterval);

            // 랜덤한 주기만큼 대기
            yield return new WaitForSeconds(randomInterval);

            if (spawnedObjects.Count < maxSpawnCount)
            {
                SpawnSingleAgent(spawnInfo);
            }
        }
    }

    // 단일 에이전트를 스폰하고 설정하는 함수
    private void SpawnSingleAgent(SpawnPointInfo spawnInfo)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnInfo.spawnTransform.position, out hit, 5.0f, NavMesh.AllAreas))
        {
            GameObject instance = Instantiate(originalPrefab, hit.position, spawnInfo.spawnTransform.rotation);
            spawnedObjects.Add(instance);

            NavMeshAgent agent = instance.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                // 2. 설정된 최소/최대 속도 사이에서 랜덤한 속도를 계산
                float randomSpeed = Random.Range(spawnInfo.minAgentSpeed, spawnInfo.maxAgentSpeed);
                agent.speed = randomSpeed; // ★★★ 랜덤 속도 설정!

                agent.SetDestination(targetPoint.position);
            }
        }
        else
        {
            Debug.LogWarning($"{spawnInfo.spawnTransform.name} 주변에서 NavMesh 위치를 찾을 수 없습니다.");
        }
    }

    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += StopSpawning;
    }

    // 오브젝트가 비활성화될 때 구독을 해제합니다. (매우 중요!)
    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= StopSpawning;
    }
}