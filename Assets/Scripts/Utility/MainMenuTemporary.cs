using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuTemporary : MonoBehaviour
{
    [SerializeField] TMP_Text debugText;
    [SerializeField] TMP_Text versionText;
    [SerializeField] Button buttonPlay;
    [SerializeField] Button exitGameBtn;

    [SerializeField] string nextScene;


    private void Start()
    {
        TransitionScreen.instance.StartingTransition(TransitionPosition.FromBlack, 1f, null);
        GameManager.instance.SoundsOnSceneManager.AllAudioFadeIn();

        versionText.SetText(Application.version);
        buttonPlay.onClick.AddListener(() =>
        { 
            GameManager.instance.SceneTransitionManager.MoveScene(nextScene);
        });

        exitGameBtn.onClick.AddListener(() =>
        {
            TransitionScreen.instance.StartingTransition(TransitionPosition.ToBlack, 1f, () =>
            {
                Application.Quit();
            });
        });
    }
}
