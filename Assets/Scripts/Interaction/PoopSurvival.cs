using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSurvival : MonoBehaviour, IInteractable
{
    // 급똥 이상 현상일 때 해당 오브젝트가 활성화되며 상호 작용이 가능함
    public TimerManager timer;
    public GameObject trigger;
    public GameObject botherTrigger;

    public void Interact()
    {
        timer.StartTimer();
        trigger.SetActive(true);
        botherTrigger.SetActive(true);

        this.enabled = false;
    }
}
