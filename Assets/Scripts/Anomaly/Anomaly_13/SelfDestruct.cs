using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [Tooltip("이 오브젝트가 파괴되기까지 걸리는 시간 (초)")]
    public float lifetime = 20f;
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);

        Destroy(gameObject);
    }
}