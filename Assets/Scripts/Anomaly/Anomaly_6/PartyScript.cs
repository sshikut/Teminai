using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyScript : MonoBehaviour, IInteractable
{
    public GameObject[] partyObjects;
    public Transform lightSwitch;
    public LightSwitch lights;
    public bool isPartyOff = true;

    public ResetAnomaly_6 end;
    public AudioSource music;
    public GameObject rds_0;

    public void Interact()
    {
        Party();
    }

    public void Party()
    {
        isPartyOff = !isPartyOff;

        AudioManager.instance.Play("LightSwitch");

        Vector3 scale = lightSwitch.localScale;
        scale.x *= -1;
        lightSwitch.localScale = scale;

        if (!isPartyOff)
        {
            music.volume = 1f;
            foreach (GameObject party in partyObjects)
            {
                party.SetActive(true);
                lights.Switch();
                rds_0.SetActive(false);
            }
        }
        else
        {
            music.volume = 0f;
            foreach (GameObject party in partyObjects)
            {
                party.SetActive(false);
                lights.Switch();
                rds_0.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += ResetParty;
    }

    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= ResetParty;
    }

    public void ResetParty()
    {
        isPartyOff = true;
        foreach (GameObject party in partyObjects)
        {
            party.SetActive(false);
        }
        end.EndAnomaly_6();
    }
}
