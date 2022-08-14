using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectEndDialogue : MonoBehaviour
{
    public DialogueController dialogueController;

    void Update()
    {
        if (dialogueController.DialogueEnd == true)
        Destroy(gameObject);
    }
}
