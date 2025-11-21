using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Player HUD")]
    [SerializeField] Slider HPImage;

    [Header("Pause HUD Menu")]
    [SerializeField] PauseMenu pauseButton;
    [SerializeField] EndGameUIManager endGameUIManager;
    [SerializeField] GameObject panelInteract;

    // private void Awake()
    // {
    //     void OnPauseButtonClicked()
    //     {
    //         InGameTracker.instance.ChangeGameState(GameplayState.Pause);
    //     }

    //     void OnBackToGameButtonClicked()
    //     {
    //         InGameTracker.instance.ChangeGameState(GameplayState.Playing);
    //     }
    //     spawnerManager.OnEnemyDied += OnEnemiesRemainingText;
    //     pauseButton.onClick.AddListener(OnPauseButtonClicked);
    //     backGameButton.onClick.AddListener(OnBackToGameButtonClicked);
    // }

    void OnPauseButtonClicked()
    {
        if (LevelManager.instance.GetgameState() == GameplayState.Pause)
            return;
            
        LevelManager.instance.SetGameState(GameplayState.Pause);
        //pauseButton.Show();
    }

    void OnBackToGameButtonClicked()
    {
        LevelManager.instance.SetGameState(GameplayState.Playing);
        Debug.Log("Back to Game from HUDManager");
        //pauseButton.Hide();
    }
    
    void Awake()
    {
        GameManager.instance.Input.onPausePressed += OnPauseButtonClicked;
        pauseButton.onBackPressed += OnBackToGameButtonClicked;
    }

    private void Start()
    {
        HPImage.maxValue = PlayerStats.instance.startingStats.healthMax;
        HPImage.value = PlayerStats.instance.currentHealth;
    }

    private void OnDisable()
    {
        GameManager.instance.Input.onPausePressed -= OnPauseButtonClicked;
        pauseButton.onBackPressed -= OnBackToGameButtonClicked;
    }

    public void SetHealthBar(float value)
    {
        HPImage.value = value;
    }

    public void SetInteractPanel(bool state)
    {
        panelInteract.SetActive(state);

        Debug.Log("Set Interact Panel: " + state);
    }

    public void SetDeathScreen()
    {
        endGameUIManager.SetActiveState(true);
    }
}
