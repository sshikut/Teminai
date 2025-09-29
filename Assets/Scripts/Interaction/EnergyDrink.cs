using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class EnergyDrink : MonoBehaviour, IInteractable
{
    public FirstPersonController fpc;
    public float duration = 10f;
    private Coroutine destroyCoroutine;

    private void Start()
    {
        fpc = FindObjectOfType<FirstPersonController>();
        destroyCoroutine = StartCoroutine(DestroyGameobject());
    }

    public void Interact()
    {
        if (destroyCoroutine != null)
        {
            StopCoroutine(destroyCoroutine);
            destroyCoroutine = null;
        }
        DrinkEnergy();
    }

    void DrinkEnergy()
    {
        fpc.ApplySpeedBoost(10f, 12f, duration);
        Destroy(gameObject);
    }

    IEnumerator DestroyGameobject()
    {
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
}
