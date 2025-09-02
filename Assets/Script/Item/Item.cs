using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject // ���ӿ�����Ʈ�� ���� �ʿ䰡 ����
{
    public string itemName;

    [TextArea] // ����ġ�� �����ٷ� �̵��� ��������
    public string itemDesc; 
    
    public ItemType itemType;
    public Sprite itemImage; // image�� ������, �̰��� ����󿡼��� �ٿ� �� �ִ�. (ĵ���� ����)
    public GameObject itemPrefab;

    public string weaponType; // ���� ����

    public enum ItemType // ������ �ڷ���
    {
        Equipment,
        Used,
        Ingredent,
        ETC
    }
}
