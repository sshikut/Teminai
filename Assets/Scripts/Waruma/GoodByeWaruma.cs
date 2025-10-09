using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodByeWaruma : MonoBehaviour
{
    private bool playerInside = false;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 트리거 존에 들어옴!");
            playerInside = true;
            this.gameObject.SetActive(false);
        }
    }
}
