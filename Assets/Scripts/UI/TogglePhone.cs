using UnityEngine;

public class TogglePhone : MonoBehaviour
{
    [SerializeField] private Animator anim; // 폰 오브젝트 할당
    [SerializeField] private GameObject phone; // 폰 오브젝트 할당

    private bool isActive = false;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isActive = !isActive;
           

            if (isActive)
            {
                AudioManager.instance.Play("OpenPhone");
                anim.Play("Open");
            }
            else 
            {
                AudioManager.instance.Play("ClosePhone");
                anim.Play("Close");
            }
        }
    }
    public void DisableToggleAfterOneUse() // 핸드폰 ui가 켜져있지않으면 켜는 함수
    {
        if (isActive == false)
        {
            isActive = !isActive;
            AudioManager.instance.Play("OpenPhone");
            anim.Play("Open");
        }
       
     
    }
}