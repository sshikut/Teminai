using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogues;

    public string RandomDialogues()
    {
        int random = Random.Range(0, dialogues.Length);

        return dialogues[random];
    }
}
