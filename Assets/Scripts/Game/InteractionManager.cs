using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    public CharacterController characterController;

    public bool IsFading => isFading;

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

    [Header("UI 설정")]
    public TMP_Text interactionText;
    public Image fadePanel;

    [Header("포지션 이동 설정")]
    public GameObject objectToMove;
    public Vector3 targetPosition;

    [Header("Ray Setting")]
    public float rayDistance = 3f;
    public LayerMask interactableLayers;

    private bool isInteractable = false;
    private bool isFading = false;

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
        if (isFading) return;

        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayers))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                isInteractable = true;
                if (interactionText != null)
                {
                    interactionText.gameObject.SetActive(true);
                    interactionText.text = "E";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // IInteractable 인터페이스를 상속받은 스크립트에 접근 
                        interactable.Interact();
                    }
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
    }

    public void StartFadeIn()
    {
        if (fadePanel != null)
        {
            StartCoroutine(FadeEffect(1, 0, 1.5f));
            characterController.enabled = true; 
        }
    }

    public void StartFadeOut()
    {
        if (fadePanel != null)
        {
            StartCoroutine(FadeEffect(0, 1, 1.5f));
            characterController.enabled = false;
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