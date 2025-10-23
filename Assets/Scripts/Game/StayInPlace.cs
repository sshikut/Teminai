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

   
    private Vector3 initialSlowDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isEffectActive)
        {
           
            float dotProduct = Vector3.Dot(transform.forward, initialSlowDirection);

           
            if (dotProduct > 0)
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

       
        initialSlowDirection = transform.forward;

        Debug.Log("느려짐 (기준 방향 저장)");
    }


    public void StopEffect()
    {
        if (isEffectActive)
        {
            isEffectActive = false;
            Debug.Log("효과 해제 (뒤돌기)");
        }
    }
}