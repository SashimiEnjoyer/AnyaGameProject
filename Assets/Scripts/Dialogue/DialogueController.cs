using UnityEngine;
using UnityEngine.Events;

[System.Serializable] 
struct DialogueData
{
    public Sprite[] characterImage;
    public string characterName;
    [TextArea(5,20)]public string characterDialogue;
    public UnityEvent onCurrentDialogueEvent;
};

public class DialogueController : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData[] dialogues;
    [SerializeField] private GameObject dialogueModalUIPrefab;
    private GameObject dialogueObject;
    private DialogueModalUI dialogueUI;
    public bool isOpen = false;
    private int dialogueIndex = 0;
    public bool ObjectDestroyed = false;
    public bool DialogueEnd = false;

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

        dialogues[dialogueIndex].onCurrentDialogueEvent?.Invoke();
    }

    private void EndDialogue()
    {
        dialogueObject.SetActive(false);    
        ObjectDestroyed = true;
        DialogueEnd = true;
    }

}
