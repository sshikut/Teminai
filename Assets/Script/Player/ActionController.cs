using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using TMPro;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; // ���� ������ �ִ� �Ÿ�

    private bool pickupActivated = false;

    private RaycastHit hitInfo; // �浹ü ���� ����

    [SerializeField]
    private LayerMask layerMask; // ������ ���̾�� �����ϵ��� ���̾� ����ũ ����

    //private ShowText actionText; // �ʿ��� ������Ʈ

    //private Inventory theInventory;

    private void Start()
    {
        //theInventory = FindObjectOfType<Inventory>();    
        //actionText = FindAnyObjectByType<ShowText>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated) 
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ��");
                //theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
            {
                if (hitInfo.transform.tag == "Item")
                {
                    ItemInfoAppear();
                }
            }
        else
            {
            InfoDisappear();
            }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        //actionText.gameObject.SetActive(true);
        //actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ��" + "<color=yellow>"+"(E)"+"</color>";
    }

    private void InfoDisappear() 
    {
        pickupActivated = false;
        //actionText.gameObject.SetActive(false);
    }
}
