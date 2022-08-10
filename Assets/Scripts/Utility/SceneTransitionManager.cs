
using UnityEngine;
using TMPro;


public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] GameObject transitionScreenPrefab;
    [SerializeField] AudioSource mainBGM;
    [SerializeField] GameObject loadingUIPrefab;
    GameObject loadingUI;
    TMP_Text loadingProgressText;

    private string nextSceneName;

    private void Start()
    {
        GameObject go = Instantiate(transitionScreenPrefab);
        TransitionScreen.instance.StartingTransition(TransitionPosition.FromBlack, 3f, FinishedFirstTransition);
    }

    public void StartMoveScene(string sceneName)
    {
        nextSceneName = sceneName;
        GameObject go = Instantiate(transitionScreenPrefab);
        TransitionScreen.instance.StartingTransition(TransitionPosition.ToBlack, 2f, GoToNextScene);
        SoundsOnSceneManager.instance.AllAudioFadeOut();
    }

    void FinishedFirstTransition()
    {

        if(mainBGM != null)
        mainBGM.Play();

        InGameTracker.instance.gameState = GameplayState.Playing;
    }

    void GoToNextScene()
    {
        SceneLoader.LoadScene(nextSceneName, LoadingStart, LoadingProgress);
    }

    void LoadingStart()
    {
        if (loadingUI == null)
            loadingUI = Instantiate(loadingUIPrefab);

        loadingProgressText = loadingUI.GetComponentInChildren<TMP_Text>();  
    }

    void LoadingProgress(int progress)
    {
        loadingProgressText.text = "Loading... " + progress.ToString() + "%";
    }

}
