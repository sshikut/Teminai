using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour, IInteractable
{
    public bool isOn = false;

    public void Interact()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                AudioManager.instance.Play("Do");

            if (Input.GetKeyDown(KeyCode.Alpha2))
                AudioManager.instance.Play("Re");

            if (Input.GetKeyDown(KeyCode.Alpha3))
                AudioManager.instance.Play("Mi");

            if (Input.GetKeyDown(KeyCode.Alpha4))
                AudioManager.instance.Play("Fa");

            if (Input.GetKeyDown(KeyCode.Alpha5))
                AudioManager.instance.Play("Sol");

            if (Input.GetKeyDown(KeyCode.Alpha5))
                AudioManager.instance.Play("Sol");

            if (Input.GetKeyDown(KeyCode.Alpha5))
                AudioManager.instance.Play("Ra");

            if (Input.GetKeyDown(KeyCode.Alpha5))
                AudioManager.instance.Play("Si");
        }
    }
}
