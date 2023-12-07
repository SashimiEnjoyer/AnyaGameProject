using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuTemporary : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button buttonPlay;

    [SerializeField] string nextScene;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(() =>
        {
            PlayerStats.playerName = inputField.text;
            SceneManager.LoadScene(nextScene);
        });
    }

}
