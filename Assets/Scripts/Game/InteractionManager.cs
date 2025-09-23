using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    public TimerManager TimerManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("상호작용 대상")]
    public GameObject targetObject;

    [Header("UI 설정")]
    public TMP_Text interactionText;
    public Image fadePanel;

    [Header("포지션 이동 설정")]
    public GameObject objectToMove;
    public Vector3 targetPosition;

    private bool isInteractable = false;
    private bool isFading = false;

    public bool canInteract = false;
    void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
        StartFadeIn();
    }

    void Update()
    {
        // --- [수정 1] 플래그를 확인하여 Update 함수의 작동 여부를 결정 ---
        if (!canInteract)
        {
            // canInteract가 false이면 아무것도 하지 않고 즉시 종료
            return;
        }

        // 아래는 canInteract가 true일 때만 실행됩니다.
        if (targetObject == null || isFading) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == targetObject)
            {
                isInteractable = true;
                if (interactionText != null)
                {
                    interactionText.gameObject.SetActive(true);
                    interactionText.text = "화장실 빨리 가야하는 현상";
                }
            }
            else
            {
                isInteractable = false;
                if (interactionText != null)
                {
                    interactionText.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            isInteractable = false;
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }

        if (isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            gamestart();
            interactionText.text = "";
            DeactivateInteraction(); // --- [수정 2] 상호작용이 끝나면 자동으로 비활성화 ---
        }
    }


    public void ActivateInteraction()
    {
        Debug.Log("InteractionManager 활성화!");
        canInteract = true;
    }
    public void DeactivateInteraction()
    {
        Debug.Log("InteractionManager 비활성화!");
        canInteract = false;

        // UI도 깔끔하게 비활성화 처리
        isInteractable = false;
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }
    public void gamestart()
    {
        if (isFading) return;

        StartGameSequence();
    }

    private void StartGameSequence()
    {
        
     

           
        TimerManager.StartTimer();

       
    
    }
    public void StartFadeIn()
    {
        if (fadePanel != null)
        {
            StartCoroutine(FadeEffect(1, 0, 1.5f));
        }
    }

    public void StartFadeOut()
    {
        if (fadePanel != null)
        {
            StartCoroutine(FadeEffect(0, 1, 1.5f));
        }
    }

    IEnumerator FadeEffect(float startAlpha, float endAlpha, float duration)
    {
        isFading = true;

        if (fadePanel != null)
        {
            fadePanel.gameObject.SetActive(true);
            float timer = 0f;
            Color fadeColor = fadePanel.color;

            while (timer < duration)
            {
                timer += Time.deltaTime;
                fadeColor.a = Mathf.Lerp(startAlpha, endAlpha, timer / duration);
                fadePanel.color = fadeColor;
                yield return null;
            }

            fadeColor.a = endAlpha;
            fadePanel.color = fadeColor;

            if (endAlpha == 0)
            {
                fadePanel.gameObject.SetActive(false);
            }

            if (endAlpha == 1)
            {
                if (objectToMove != null)
                {
                
                    CharacterController cc = objectToMove.GetComponent<CharacterController>();

                    if (cc != null) cc.enabled = false;

                    objectToMove.transform.position = targetPosition;

                    if (cc != null) cc.enabled = true;

                    Debug.Log($"{objectToMove.name}의 위치를 {targetPosition}으로 변경했습니다.");
                }

                StartFadeIn();
            }
        }

        if (endAlpha == 0)
        {
            isFading = false;
        }
        
    }
}