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
    public void SetDialogueUI(string _nameText, string _dialogueText, Sprite[] _dialogueimage)
    {
        nameText.text = _nameText;
        dialogueText.text = _dialogueText;

        foreach (GameObject g in images)
            g.SetActive(false);
        
        for (int i = 0; i < _dialogueimage.Length; i++)
        {
            images[i].SetActive(true);
            images[i].GetComponentInChildren<Image>().sprite = _dialogueimage[i];
        }
    }


    public void NextDialogue()
    {
        OnNextDialogueButtonPressed?.Invoke();
    }
}
