using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorQTE : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leftDoor;
    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += resetDoor;
    }

    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= resetDoor;
    }
    public void resetDoor()
    {
        int layerIndex = LayerMask.NameToLayer("Interactable");
        leftDoor.layer = layerIndex;
    }
}
