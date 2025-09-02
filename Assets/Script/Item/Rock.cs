using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp; // 바위 체력

    [SerializeField]
    private float destroyTime; // 파편 제거

    [SerializeField]
    private SphereCollider col; // 구체 콜라이더

    // 필요한 게임 오브젝트
    [SerializeField]
    private GameObject go_rock; // 일반 바위
    [SerializeField]    
    private GameObject go_debirs; // 깨진 바위
    [SerializeField]
    private GameObject go_effect_prefabs; // 채굴 이팩트
    [SerializeField]
    private GameObject go_rock_item_prefabs;

    // 돌멩이 아이템 등장 개수
    [SerializeField]
    private int count;


    // 필요한 사운드 네임
    [SerializeField]
    private string strike_Sound;
    [SerializeField]
    private string destroy_Sound;


    public void Mining()
    {
        //SoundManager.instance.PlaySE(strike_Sound);
        var clone = Instantiate(go_effect_prefabs, col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);

        //Debug.Log("Mining() 호출됨, 현재 hp: " + hp);
        hp--;
        if (hp <= 0)
            Destrucion();
    }



    private void Destrucion()
    {
        //SoundManager.instance.PlaySE(destroy_Sound);
        col.enabled = false;

        for (int i = 0; i < count; i++)
        {
            Instantiate(go_rock_item_prefabs, go_rock.transform.position, Quaternion.identity);
        }

        Destroy(go_rock);   

        go_debirs.SetActive(true);
        Destroy(go_debirs, destroyTime); // 일정 시간 유예 후 삭제
    }


}
