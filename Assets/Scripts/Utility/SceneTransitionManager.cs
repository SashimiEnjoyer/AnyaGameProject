
using UnityEngine;
using TMPro;


public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] GameObject transitionScreenPrefab;
    [SerializeField] AudioSource mainBGM;
    [SerializeField] GameObject loadingUIPrefab;
    GameObject loadingUI;
    TMP_Text loadingProgressText;
    GameObject transistionObject;

    private string nextSceneName;

    private void Start()
    {
        if(transistionObject == null)
            transistionObject = Instantiate(transitionScreenPrefab);

        TransitionScreen.instance.StartingTransition(TransitionPosition.FromBlack, 3f, FinishedFirstTransition);
    }

    public void MoveScene(string sceneName)
    {
        if (transistionObject == null)
            transistionObject = Instantiate(transitionScreenPrefab);
        
        nextSceneName = sceneName;
        TransitionScreen.instance.StartingTransition(TransitionPosition.ToBlack, 2f, GoToNextScene);
        SoundsOnSceneManager.instance.AllAudioFadeOut();
    }

    void FinishedFirstTransition()
    {

        if(mainBGM != null)
        mainBGM.Play();

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
