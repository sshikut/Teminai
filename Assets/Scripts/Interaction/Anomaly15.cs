using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anomaly15 : MonoBehaviour, IInteractable
{
    public Animator animator;
    private string name = "Stand";

    public void Interact()
    {
        animator.SetBool(name, true);
    }

}
