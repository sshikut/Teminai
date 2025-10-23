using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anomaly15 : Communication
{
    public Animator animator;
    private string animName = "Stand";

    public override void Interact()
    {
        base.Interact();
        animator.SetBool(animName, true);
    }

}
