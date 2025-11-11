using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Player HUD")]
    [SerializeField] Slider HPImage;
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text enemyRemainingText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] EnemySpawnManager spawnerManager;

    [Header("Pause HUD Menu")]
    [SerializeField] Button pauseButton;
    [SerializeField] Button backGameButton;

    int temp = 18;
    
    public static float counter = 0f;

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

    private void Start()
    {
        counter = 0f;
        HPImage.maxValue = PlayerStats.instance.startingStats.healthMax;
        HPImage.value = PlayerStats.instance.currentHealth;
    }

    private void OnDisable()
    {
        spawnerManager.OnEnemyDied -= OnEnemiesRemainingText;
    }

    public void SetHealthBar(float value)
    {
        HPImage.value = value;
    }

    void OnEnemiesRemainingText()
    {
        temp --;
        enemyRemainingText.SetText($"Remaining Enemies: {temp}");
    }

    private void OnDestroy()
    {
        pauseButton.onClick.RemoveAllListeners();
        backGameButton.onClick.RemoveAllListeners();    
    }
}
