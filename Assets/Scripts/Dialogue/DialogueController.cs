using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] 
struct DialogueData
{
    public Sprite[] characterImage;
    public string characterName;
    [TextArea(5,20)]public string characterDialogue;
};

public class DialogueController : MonoBehaviour, IInteractable
{
    [SerializeField] DialogueData[] dialogues;
    [SerializeField] GameObject dialogueModalUIPrefab;
    GameObject dialogueObject;
    DialogueModalUI dialogueUI;
    bool isOpen = false;
    int dialogueIndex = 0;

    public void ExecuteInteractable()
    {
        if (!isOpen)
        {
            isOpen = true;

            dialogueObject = Instantiate(dialogueModalUIPrefab);
            dialogueUI = dialogueObject.GetComponent<DialogueModalUI>();
            dialogueUI.SetDialogueUI(dialogues[0].characterName, dialogues[0].characterDialogue, dialogues[0].characterImage);
            dialogueUI.OnNextDialogueButtonPressed += NextDialogue;
        }
    }

    public void NextDialogue()
    {
        dialogueIndex += 1;

        if (dialogueIndex <= dialogues.Length - 1)
            dialogueUI.SetDialogueUI(dialogues[dialogueIndex].characterName, dialogues[dialogueIndex].characterDialogue, dialogues[dialogueIndex].characterImage);
        else
        {
            dialogueUI.OnNextDialogueButtonPressed -= NextDialogue;
            Destroy(dialogueObject);
        }
    }
}
