using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUIManager : MonoBehaviour
{
    [SerializeField] GameObject uiobject;
    [SerializeField] TMP_Text headerTest;
    [SerializeField] Button backToMainMenuBtn;
    [Space]
    [SerializeField] GameObject bestScoreObject;
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text timeStamp;
    [SerializeField] TMP_Text healthRemaining;
    [Space]
    [SerializeField] GameObject currentScore;
    [SerializeField] TMP_Text currentPlayerName;
    [SerializeField] TMP_Text currentTimeStamp;
    [SerializeField] TMP_Text currentHealth;

    public void SetUI(bool isWinning)
    {
        uiobject.SetActive(true);
        if (isWinning)
            headerTest.SetText("GAME FINISHED! \n\n CONGRATULATIONS!");
        else
            headerTest.SetText("YOU DIED!");

        backToMainMenuBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Main Menu_For Demo");
        });
    }

    public void SetBestScore(SaveData data)
    {
        bestScoreObject.SetActive(true);
        playerName.SetText(data.playerName);
        timeStamp.SetText(data.timeStamp.ToString());
        healthRemaining.SetText(data.remainingHP.ToString());
    }

    public void SetCurrentScore(SaveData data)
    {
        currentScore.SetActive(true);
        currentPlayerName.SetText(data.playerName);
        currentTimeStamp.SetText(data.timeStamp.ToString());
        currentHealth.SetText(data.remainingHP.ToString());
    }
}
