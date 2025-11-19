using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private SkillManager skillManager;
    [SerializeField] private LevelSectionManager[] levelSections;

    private int currentSectionIndex = -1;
    private bool levelInitiated = false;
    public int CurrentSectionIndex => currentSectionIndex;

    void Awake()
    {
        skillManager.InstantiateSkills();
        
        foreach (var section in levelSections)
        {
            section.CleanupSection();
        }


        levelInitiated = true;

        TransitionScreen.instance.StartingTransition(TransitionPosition.FromBlack, 1f, null);
    }

    void Start()
    {
        SetCurrentSectionIndex(0);
        GameManager.instance.SoundsOnSceneManager.AllAudioFadeIn();
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
}
