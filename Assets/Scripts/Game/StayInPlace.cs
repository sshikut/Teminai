using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class StayInPlace : MonoBehaviour
{
    [Tooltip("뒤로 밀려나는 힘의 비율입니다.")]
    [Range(0f, 1f)]
    public float cancellationFactor = 0.8f;

    private bool isEffectActive = false;
    private CharacterController controller;
    private Vector3 lastPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
       
        if (isEffectActive)
        {
         
            float forwardDot = Vector3.Dot(transform.forward, Vector3.forward);

          
            if (forwardDot > 0)
            {
               
                Vector3 movementThisFrame = transform.position - lastPosition;
                Vector3 counterMovement = -movementThisFrame * cancellationFactor;
                controller.Move(counterMovement);
            }
            else
            {
               
                StopEffect();
            }

           
            lastPosition = transform.position;
        }
    }

    
    public void StartEffect()
    {
        isEffectActive = true;
        lastPosition = transform.position;
        Debug.Log("느려짐");
    }

  
    public void StopEffect()
    {
        
        if (isEffectActive)
        {
            isEffectActive = false;
            Debug.Log("뒤돌기");
        }
    }
}