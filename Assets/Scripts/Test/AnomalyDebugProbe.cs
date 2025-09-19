using UnityEngine;

public class AnomalyDebugProbe : MonoBehaviour
{
    [SerializeField] private AnomalyManager manager;

    void Awake()
    {
        if (manager == null) manager = FindObjectOfType<AnomalyManager>();
        Debug.Log($"[Probe] ManagerRef={manager?.GetInstanceID()} this={GetInstanceID()}");
    }

    void Update()
    {
        if (manager == null) return;
        int current = manager.maxAnomalies - manager.remainAnomaly;
        Debug.Log($"[Probe] current={current}, remain={manager.remainAnomaly}, max={manager.maxAnomalies}");
    }
}