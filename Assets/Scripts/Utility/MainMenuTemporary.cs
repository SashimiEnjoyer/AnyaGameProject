using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuTemporary : MonoBehaviour
{
    [SerializeField] private SceneTransitionManager transitionManager;
    [SerializeField] TMP_Text debugText;
    [SerializeField] TMP_Text versionText;
    [SerializeField] Button buttonPlay;

    [SerializeField] string nextScene;


    private void Start()
    {
        TransitionScreen.instance.StartingTransition(TransitionPosition.FromBlack, 1f, null);
        SoundsOnSceneManager.instance.AllAudioFadeIn();

        versionText.SetText(Application.version);
        buttonPlay.onClick.AddListener(() =>
        { 
            transitionManager.MoveScene(nextScene);
        });
    }
}
