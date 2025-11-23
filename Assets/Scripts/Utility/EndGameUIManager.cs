using UnityEngine;
using UnityEngine.UI;

public class EndGameUIManager : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    void Start()
    {
        restartButton.onClick.AddListener(() =>
        {
            LevelManager.instance.GoToScene(GameManager.instance.GetCurrentSceneName());
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            LevelManager.instance.GoToMainMenu();
        });

        //SetActiveState(false);
    }
    
    public void SetActiveState(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
