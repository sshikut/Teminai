using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance;

    [Header("상호작용 대상")]
    public GameObject targetObject;

    [Header("UI 설정")]
    public TMP_Text interactionText;
    public Image fadePanel;

    // ✨ 1. 위치 이동에 필요한 변수들 추가
    [Header("포지션 이동 설정")]
    public GameObject objectToMove; // 이동시킬 오브젝트
    public Vector3 targetPosition;   // 목표 위치 좌표

    private bool isInteractable = false;
    private bool isFading = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
                    interactionText.text = "E 키를 눌러 위치 이동"; // 텍스트 변경
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
            StartFadeOut();
        }
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

            // ✨ 2. 페이드 아웃 완료 시 씬 로드 대신 위치 변경 로직 실행
            if (endAlpha == 1)
            {
                // objectToMove 변수가 할당되었는지 확인
                if (objectToMove != null)
                {
                    // 할당된 오브젝트의 위치를 targetPosition으로 변경
                    objectToMove.transform.position = targetPosition;
                    Debug.Log($"{objectToMove.name}의 위치를 {targetPosition}으로 변경했습니다.");
                }

                // ✨ 3. 위치 변경 후, 다시 페이드 인하여 바뀐 모습을 보여줌
                StartFadeIn();
            }
        }

        // 페이드 아웃의 경우, 페이드 인이 시작되므로 isFading은 아직 true로 유지
        if (endAlpha == 0)
        {
            isFading = false;
        }
    }
}