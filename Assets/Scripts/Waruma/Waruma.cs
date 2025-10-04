using UnityEngine;

public class Waruma : MonoBehaviour
{
    public Animator anim;
    public Collider absentCol;

    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Front"))
        {
            absentCol.enabled = true;   // 앞 봄 → 패널티 가능
        }
        else if (stateInfo.IsName("Back"))
        {
            absentCol.enabled = false;  // 뒤 봄 → 패널티 없음
        }
    }
}