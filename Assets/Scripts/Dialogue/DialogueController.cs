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
    [SerializeField] private DialogueData[] dialogues;
    [SerializeField] private GameObject dialogueModalUIPrefab;
    private GameObject dialogueObject;
    private DialogueModalUI dialogueUI;
    private bool isOpen = false;
    private int dialogueIndex = 0;
    private bool ObjectDestroyed = false;

    private void Start()
    {
        InGameTracker.instance.onGameStateChange += TestChange;
    }

    private void OnDestroy()
    {
        InGameTracker.instance.onGameStateChange -= TestChange;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isOpen == true && ObjectDestroyed == false)
        {
            NextDialogue();
        }
    }

    public void ExecuteInteractable()
    {
        if (!isOpen)
        {
            isOpen = true;

            dialogueObject = Instantiate(dialogueModalUIPrefab);
            dialogueUI = dialogueObject.GetComponent<DialogueModalUI>();
            dialogueUI.SetDialogueUI(dialogues[0].characterName, dialogues[0].characterDialogue, dialogues[0].characterImage);
            dialogueUI.OnNextDialogueButtonPressed += NextDialogue;
            InGameTracker.instance.gameState = GameplayState.Dialogue;
        }
    }

    private void NextDialogue()
    {
        dialogueIndex += 1;

        if (dialogueIndex <= dialogues.Length - 1)
            dialogueUI.SetDialogueUI(dialogues[dialogueIndex].characterName, dialogues[dialogueIndex].characterDialogue, dialogues[dialogueIndex].characterImage);
        else
        {
            dialogueUI.OnNextDialogueButtonPressed -= NextDialogue;
            EndDialogue();
            InGameTracker.instance.gameState = GameplayState.Playing;
            dialogueIndex = 0;
        }
    }

    private void EndDialogue()
    {
        Destroy(dialogueObject);
        ObjectDestroyed = true;
    }

    public void TestChange(GameplayState state)
    {
        Debug.Log("State: " + state);
    }

}
