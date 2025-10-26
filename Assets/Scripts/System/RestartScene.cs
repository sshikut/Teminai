using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }
        if (AudioManager.instance != null)
        {
            Destroy(AudioManager.instance.gameObject);
        }
        if (BGMManager.instance != null)
        {
            Destroy(BGMManager.instance.gameObject);
        }

        SceneManager.LoadScene(0);
    }
}
