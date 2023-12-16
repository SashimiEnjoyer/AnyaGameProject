using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Player HUD")]
    [SerializeField] Image HPImage;
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text enemyRemainingText;
    [SerializeField] EnemySpawnManager spawnerManager;

    [Header("Pause HUD Menu")]
    [SerializeField] Button pauseButton;
    [SerializeField] Button backGameButton;

    int temp = 18;

    private void Awake()
    {
        void OnPauseButtonClicked()
        {
            InGameTracker.instance.ChangeGameState(GameplayState.Pause);
        }

        void OnBackToGameButtonClicked()
        {
            InGameTracker.instance.ChangeGameState(GameplayState.Playing);
        }
        spawnerManager.OnEnemyDied += OnEnemiesRemainingText;
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        backGameButton.onClick.AddListener(OnBackToGameButtonClicked);
    }

    private void OnDisable()
    {
        spawnerManager.OnEnemyDied -= OnEnemiesRemainingText;
    }

    // Update is called once per frame
    void Update()
    {
        HPImage.fillAmount = PlayerStats.instance.playerHealth / 3;
    }

    void OnEnemiesRemainingText()
    {
        temp = -1;
        enemyRemainingText.SetText($"Remaining Enemies: {temp}");
    }

    private void OnDestroy()
    {
        pauseButton.onClick.RemoveAllListeners();
        backGameButton.onClick.RemoveAllListeners();    
    }
}
