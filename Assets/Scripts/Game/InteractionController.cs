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
        // 텍스트는 비활성화
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }

        // ✨ 게임 시작 시 페이드 인 효과를 바로 시작
        StartFadeIn();
    }

    void Update()
    {
        // 페이드 효과가 진행 중일 때는 어떤 입력도 받지 않음
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
                    interactionText.text = "E 키를 눌러 페이드 아웃";
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

    // ✨ 페이드 인을 시작하는 함수
    public void StartFadeIn()
    {
        if (fadePanel != null)
        {
            StartCoroutine(FadeEffect(1, 0, 1.5f)); // 1(불투명) -> 0(투명)
        }
    }

    // 페이드 아웃을 시작하는 함수
    public void StartFadeOut()
    {
        if (fadePanel != null)
        {
            StartCoroutine(FadeEffect(0, 1, 1.5f)); // 0(투명) -> 1(불투명)
        }
    }

    // ✨ 페이드 인/아웃 효과를 모두 처리하는 범용 코루틴
    IEnumerator FadeEffect(float startAlpha, float endAlpha, float duration)
    {
        isFading = true; // 페이드 시작

        if (fadePanel != null)
        {
            fadePanel.gameObject.SetActive(true);

            float timer = 0f;
            Color fadeColor = fadePanel.color;

            while (timer < duration)
            {
                timer += Time.deltaTime;
                // Lerp 함수를 사용해 시작 알파 값에서 목표 알파 값으로 부드럽게 변경
                fadeColor.a = Mathf.Lerp(startAlpha, endAlpha, timer / duration);
                fadePanel.color = fadeColor;
                yield return null;
            }

            // 목표 알파 값으로 정확하게 설정
            fadeColor.a = endAlpha;
            fadePanel.color = fadeColor;

            // 페이드 인(endAlpha가 0)이 끝났다면, 패널을 비활성화하여 성능 확보
            if (endAlpha == 0)
            {
                fadePanel.gameObject.SetActive(false);
            }

            // 페이드 아웃(endAlpha가 1)이 끝났다면, 다음 동작 수행
            if (endAlpha == 1)
            {
                Debug.Log("페이드 아웃 완료! 씬을 다시 시작합니다.");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        isFading = false; // 페이드 종료
    }
}