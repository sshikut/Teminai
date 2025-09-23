using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{

    public UnityEvent onTriggerEnter;
    public string targetTag = "Player";
    public bool triggerOnce = false;
    public bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
      

        if (other.CompareTag(targetTag) && triggerOnce == true)
        {

            onTriggerEnter.Invoke();


            hasTriggered = true;
        }
    }

}