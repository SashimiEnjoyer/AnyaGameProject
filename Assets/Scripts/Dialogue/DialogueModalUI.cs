using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueModalUI : MonoBehaviour
{
    public Action OnNextDialogueButtonPressed;
    
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] Button nextButton;
    [SerializeField] GameObject[] images;
    
    public void SetDialogueUI(DialogueData _data)
    {
        nextButton.onClick.RemoveListener(NextDialogue);
        nextButton.onClick.AddListener(NextDialogue);
        nameText.text = _data.currentCharacterName;
        dialogueText.text = _data.characterDialogue;

        foreach (GameObject g in images)
            g.SetActive(false);
        
        for (int i = 0; i <= _data.imagesSetting.Length - 1; i++)
        {
            images[i].SetActive(true);
            images[i].GetComponentInChildren<Image>().sprite = _data.imagesSetting[i].characterImage;
            images[i].GetComponentInChildren<Transform>().localScale = Vector3.one * _data.imagesSetting[i].imageSize01;
        }
    }

    public void NextDialogue()
    {
        OnNextDialogueButtonPressed?.Invoke();
    }
}
