using UnityEngine;

public class AudioFilterTrigger : MonoBehaviour
{
    public AudioLowPassFilter classroomMusicFilter;

    public float muffledFrequency = 200f;
    public float clearFrequency = 22000f;
    public float transitionSpeed = 3.0f;
    private float targetFrequency;

    void Start()
    {
        if (classroomMusicFilter == null)
        {
            Debug.LogError("Audio Low Pass Filter가 할당되지 않았습니다!", this);
        }

        targetFrequency = muffledFrequency;
        classroomMusicFilter.cutoffFrequency = muffledFrequency;
    }

    void Update()
    {
        if (classroomMusicFilter != null)
        {
            classroomMusicFilter.cutoffFrequency = Mathf.Lerp(
                classroomMusicFilter.cutoffFrequency,
                targetFrequency,
                Time.deltaTime * transitionSpeed
            );
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetFrequency = clearFrequency;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetFrequency = muffledFrequency;
        }
    }
}