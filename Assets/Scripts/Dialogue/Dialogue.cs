using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public Sprite currentCharacterImage;
    public string CharacterName;
    [TextArea(5,20)]
    public string dialogueText;
}
