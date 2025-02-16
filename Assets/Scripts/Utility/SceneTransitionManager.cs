
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] GameObject transitionScreenPrefab;
    [SerializeField] AudioUtility mainBGM;
    [SerializeField] GameObject loadingUIPrefab;
    GameObject loadingUI;
    TMP_Text loadingProgressText;
    GameObject transistionObject;

    private string nextSceneName;

    private void Start()
    {
        
        if(transistionObject == null)
            transistionObject = Instantiate(transitionScreenPrefab);

        TransitionScreen.instance.StartingTransition(TransitionPosition.FromBlack, 1f, FinishedFirstTransition);
    }

    public void MoveScene(string sceneName)
    {
        if (transistionObject == null)
            transistionObject = Instantiate(transitionScreenPrefab);
        
        nextSceneName = sceneName;
        TransitionScreen.instance.StartingTransition(TransitionPosition.ToBlack, 2f, GoToNextScene);
        SoundsOnSceneManager.instance.AllAudioFadeOut();

        Debug.Log("Moving Scene");
    }

    void FinishedFirstTransition()
    {
        SoundsOnSceneManager.instance.AllAudioFadeIn();
        InGameTracker.instance.gameState = GameplayState.Playing;
        Destroy(transistionObject);
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

    void LoadingProgress(float progress)
    {
        loadingProgressText.text = "Loading... " + progress.ToString() + "%";
        Debug.Log(progress);
    }

}
