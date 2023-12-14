using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string playerName;
    public float timeStamp;
    public float remainingHP;
}
public class EndGameManager : MonoBehaviour
{
    [SerializeField] EndGameUIManager uiEndGame;
    SaveData currentData;
    bool showBestScore = false;

    private void Awake()
    {
        currentData = new SaveData();
    }
    private void Start()
    {
        InGameTracker.instance.onWinEnding += TriggerWinEnding;
        InGameTracker.instance.onLoseEnding += TriggerLoseEnding;
    }

    private void OnDisable()
    {
        InGameTracker.instance.onWinEnding -= TriggerWinEnding;
        InGameTracker.instance.onLoseEnding -= TriggerLoseEnding;
    }

    public void TriggerWinEnding()
    {
        InGameTracker.instance.ChangeGameState(GameplayState.Stop);
        CheckSaveData();
        SetCurrentWinData();
        CheckBestScore();
        
        uiEndGame.SetUI(true);
        
    }

    public void TriggerLoseEnding()
    {
        InGameTracker.instance.ChangeGameState(GameplayState.Stop);
        CheckSaveData();
        SaveData lastBestScore = SaveSystem.LoadData<SaveData>("BEST_PLAYER");
        uiEndGame.SetBestScore(lastBestScore);
        uiEndGame.SetUI(false);
    }

    private void CheckSaveData()
    {
        if( SaveSystem.LoadData<SaveData>("BEST_PLAYER") == null)
        {
            Debug.LogWarning("No Save");
            return;
        }
        showBestScore = true;

    }

    private void SetCurrentWinData()
    {
        currentData.playerName = PlayerStats.playerName;
        currentData.timeStamp = Time.time;
        currentData.remainingHP = PlayerStats.instance.playerHealth;
        uiEndGame.SetCurrentScore(currentData);
    }

    private void CheckBestScore()
    {
        SaveData lastBestScore = SaveSystem.LoadData<SaveData>("BEST_PLAYER");

        if (lastBestScore == null)
        {
            SaveSystem.Save("BEST_PLAYER", currentData);
            uiEndGame.SetBestScore(currentData, true);
            return;
        }

        if (currentData.timeStamp < lastBestScore.timeStamp ||
            currentData.timeStamp == lastBestScore.timeStamp && currentData.remainingHP > lastBestScore.remainingHP)
        {
            SaveSystem.Save("BEST_PLAYER", currentData);
            uiEndGame.SetBestScore(currentData, true);
            return;
        }

        uiEndGame.SetBestScore(lastBestScore);

    }
}
