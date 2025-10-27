using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }
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

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
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
                return;
            }
        }

        isInteractable = false;
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    public void StartFadeIn()
    {
        if (fadePanel != null)
        {
            StartCoroutine(FadeEffect(1, 0, 1.5f));
            if (characterController) characterController.enabled = true;
        }
    }

    public void StartFadeOut()
    {
        if (fadePanel != null)
        {
            StartCoroutine(FadeEffect(0, 1, 1.5f));
            if (characterController) characterController.enabled = false;
        }
    }

    // ★ 추가
    public void StartFadeOut(Action onBlack)
    {
        if (fadePanel != null)
            StartCoroutine(FadeOutRoutine(onBlack));
    }

    // ★ 추가
    private IEnumerator FadeOutRoutine(Action onBlack)
    {
        if (characterController) characterController.enabled = false;

        // 기존 로직 그대로 활용(완전 검정까지)
        yield return StartCoroutine(FadeEffect(0, 1, 1.5f));

        // 실제로 검정이 그려진 다음 프레임을 보장
        yield return new WaitForEndOfFrame(); // 필요시 yield return null; 추가 가능

        onBlack?.Invoke();
        // 이후 페이드인은 기존 FadeEffect(endAlpha==1)에서 이미 호출됨
        // (이 줄에서 다시 StartFadeIn() 호출할 필요 없음)
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

            if (Mathf.Approximately(endAlpha, 0f))
            {
                fadePanel.gameObject.SetActive(false);
            }

            if (Mathf.Approximately(endAlpha, 1f))
            {
                if (objectToMove != null)
                {
                    CharacterController cc = objectToMove.GetComponent<CharacterController>();
                    if (cc != null) cc.enabled = false;

                    objectToMove.transform.position = targetPosition;

                    if (cc != null) cc.enabled = true;

                    Debug.Log($"{objectToMove.name}의 위치를 {targetPosition}으로 변경했습니다.");
                }

                // 기존 동작 유지: 검정 도달 즉시 페이드인 시작
                StartFadeIn();
            }
        }

        if (Mathf.Approximately(endAlpha, 0f))
        {
            isFading = false;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 스크립트가 비활성화될 때 이벤트 등록 해제 (메모리 누수 방지)
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드될 때마다 이 함수가 자동으로 호출됩니다.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "크레딧 연출")
        {
            Debug.Log("참조를 다시 연결합니다.");
            characterController = FindObjectOfType<CharacterController>();
            CreditSceneRefs sceneRefs = FindObjectOfType<CreditSceneRefs>();

            if (sceneRefs != null)
            {
                // 찾은 스크립트에 미리 연결된 참조를 가져옵니다.
                fadePanel = sceneRefs.fadePanelRef;
                interactionText = sceneRefs.interactionTextRef;

                // Null 체크 (성공 확인)
                if (fadePanel == null)
                {
                    Debug.LogError("CreditSceneRefs에 fadePanel이 연결되지 않았습니다!");
                }
            }
            else
            {
                Debug.LogError("크레딧 씬에서 'CreditSceneRefs' 스크립트를 찾을 수 없습니다!");
            }

            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
            StartFadeIn();
        }
    }
}
