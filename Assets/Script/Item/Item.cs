using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject // 게임오브젝트에 붙일 필요가 없음
{
    public string itemName;

    [TextArea] // 엔터치면 다음줄로 이동이 가능해짐
    public string itemDesc; 
    
    public ItemType itemType;
    public Sprite itemImage; // image와 차이점, 이것은 월드상에서도 뛰울 수 있다. (캔버스 없이)
    public GameObject itemPrefab;

    public string weaponType; // 무기 유형

    public enum ItemType // 열거형 자료형
    {
        Equipment,
        Used,
        Ingredent,
        ETC
    }
}
