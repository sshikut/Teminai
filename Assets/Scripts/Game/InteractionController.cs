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

    [Header("포지션 이동 설정")]
    public GameObject objectToMove;
    public Vector3 targetPosition;

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
                    interactionText.text = "E 키를 눌러 위치 이동";
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