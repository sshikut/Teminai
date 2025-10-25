using UnityEngine;

public class ResetAnomaly_6 : MonoBehaviour
{
    public Collider lightSwitch;
    public AudioSource music;

    public void StartAnomaly_6()
    {
        lightSwitch.enabled = false;
        music.volume = 0f;
    }

    public void EndAnomaly_6()
    {
        lightSwitch.enabled = true;
        music.volume = 0f;
    }

    
}
