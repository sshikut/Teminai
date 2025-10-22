using UnityEngine;

public class ResetAnomaly_6 : MonoBehaviour
{
    public Collider lightSwitch;

    public void StartAnomaly_6()
    {
        lightSwitch.enabled = false;

    }

    public void EndAnomaly_6()
    {
        lightSwitch.enabled = true;
    }
}
