using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct DialogueImages
{
    public Sprite characterImage;
    public float imageSize01;
}

[System.Serializable] 
public struct DialogueData
{
    public DialogueImages[] imagesSetting;   
    public int characterHighlightIndex;
    public string currentCharacterName;
    [TextArea(5,20)]public string characterDialogue;
    public UnityEvent onCurrentDialogueEvent;
};

public class DialogueController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject dialogueModalUIPrefab;
    private GameObject dialogueObject;
    private DialogueModalUI dialogueUI;
    public bool isOpen = false;
    private int dialogueIndex = 0;
    public bool ObjectDestroyed = false;
    public bool DialogueEnd = false;
    [Space]
    [SerializeField] private DialogueData[] dialogues;
    [Space]
    public UnityEvent onDialogueEnded;

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
            dialogueUI.SetDialogueUI(dialogues[0]);
            dialogueUI.OnNextDialogueButtonPressed += NextDialogue;
            LevelManager.instance.SetGameState(GameplayState.Dialogue);
        }
    }

    private void NextDialogue()
    {
        dialogueIndex += 1;

        if (dialogueIndex < dialogues.Length)
            dialogueUI.SetDialogueUI(dialogues[dialogueIndex]);
        else
        {
            EndDialogue();
        }

        dialogues[dialogueIndex].onCurrentDialogueEvent?.Invoke();
    }

    private void EndDialogue()
    {
        dialogueUI.OnNextDialogueButtonPressed -= NextDialogue;
        onDialogueEnded?.Invoke();
        dialogueObject.SetActive(false);
        gameObject.SetActive(false);
        ObjectDestroyed = true;
        DialogueEnd = true;
        LevelManager.instance.SetGameState(GameplayState.Playing);
        dialogueIndex = 0;
    }

}
