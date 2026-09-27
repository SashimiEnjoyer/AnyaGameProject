using UnityEngine;
using UnityEngine.UI;

public class TemporaryEnding : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;

    void Awake()
    {
        mainMenuButton.onClick.AddListener(()=> GameManager.instance.SceneTransitionManager.MoveScene("Main Menu"));
    }

    private void Start()
    {
        TransitionScreen.instance.StartingTransition(TransitionPosition.FromBlack, 1f, null);
    }
}
