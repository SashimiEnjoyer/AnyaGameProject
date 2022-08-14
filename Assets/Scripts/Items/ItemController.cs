using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public DialogueController dialogueController;

    private void Update()
    {
        if (dialogueController.DialogueEnd == true)
        {
            Destroy(gameObject);
        }
    }
}
