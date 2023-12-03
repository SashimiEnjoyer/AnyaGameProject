using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Player HUD")]
    [SerializeField] Image HPImage;
    [SerializeField] PlayerController playerController;

    [Header("Pause HUD Menu")]
    [SerializeField] Button pauseButton;
    [SerializeField] Button backGameButton;

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

        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        backGameButton.onClick.AddListener(OnBackToGameButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        HPImage.fillAmount = PlayerStats.instance.playerHealth / 3;
    }

    private void OnDestroy()
    {
        pauseButton.onClick.RemoveAllListeners();
        backGameButton.onClick.RemoveAllListeners();    
    }
}
