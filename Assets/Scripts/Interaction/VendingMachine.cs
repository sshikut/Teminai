
using UnityEngine;

public class VendingMachine : MonoBehaviour, IInteractable
{
    public GameObject drink;
    public Transform target;

    public void Interact()
    {
        Vending();
    }

    void Vending()
    {
        Instantiate(drink, target.position, drink.transform.rotation);
    }
}
