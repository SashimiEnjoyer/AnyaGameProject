using System;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private SkillManager skillManager;
    [SerializeField] private HUDManager hudManager;
    [SerializeField] private EndGameUIManager deadUIManager;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private InGameTracker inGameTracker;
    [SerializeField] private LevelSectionManager[] levelSections;
    private int currentSectionIndex = -1;
    private bool levelInitiated = false;

    public int CurrentSectionIndex => currentSectionIndex;
    public InGameTracker InGameTracker => inGameTracker;
    public HUDManager HUDManager => hudManager;
    public EndGameUIManager DeadUIManager => deadUIManager;
    public SkillManager SkillManager => skillManager;

    public Action<GameplayState> onGameStateChange;

    void Awake()
    {
        if(instance == null)
            instance = this;

        
        foreach (var section in levelSections)
        {
            section.CleanupSection();
        }

        //levelInitiated = true;

    }

    void Start()
    {
        skillManager.InstantiateSkills();
        SetCurrentSectionIndex(0);

        GameManager.instance.SoundsOnSceneManager.AllAudioFadeIn();
        TransitionScreen.instance.StartingTransition(TransitionPosition.FromBlack, 1f, null);
        
    }

    public void SetCurrentSectionIndex(int index)
    {
        if(index >= 0 && index < levelSections.Length)
        {
            if(currentSectionIndex != index)
            {
                levelSections[index].InitializeSection();
                currentSectionIndex = index;
            }
        }
        else
        {
            Debug.LogWarning("Invalid section index set in LevelManager.");
        }
    }

    public void GoToMainMenu()
    {
        GameManager.instance.SceneTransitionManager.MoveScene(mainMenuSceneName);
    } 

    public void GoToScene(string sceneName)
    {
        GameManager.instance.SceneTransitionManager.MoveScene(sceneName);
    }

    public void SetGameState(GameplayState state)
    {
        inGameTracker.ChangeGameState(state);
        onGameStateChange?.Invoke(state);
        OngameStateChange(state);
    }

    public GameplayState GetgameState()
    {
        return inGameTracker.gameState;
    }

    private void OngameStateChange(GameplayState state)
    {
        Debug.Log("Level Manager Detected Game State Change: " + state);
        switch(state)
        {
            case GameplayState.Playing:
                pauseMenu.Hide();
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case GameplayState.Pause:
                pauseMenu.Show();
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameplayState.Dialogue:
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameplayState.Died:  
                deadUIManager.SetActiveState(true);
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }
}
