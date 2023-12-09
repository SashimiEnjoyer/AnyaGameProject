using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuTemporary : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text debugText;
    [SerializeField] Button buttonPlay;

    [SerializeField] string nextScene;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(() =>
        {
            PlayerStats.playerName = inputField.text;
            if(string.IsNullOrWhiteSpace(inputField.text))
            {
                return;
            }
            SceneManager.LoadScene(nextScene);
        });
    }

    public void CheckInputField(string check)
    {
        if (string.IsNullOrWhiteSpace(check))
            debugText.SetText( "Insert Username!");
        else
            debugText.SetText(string.Empty);
    }
}
