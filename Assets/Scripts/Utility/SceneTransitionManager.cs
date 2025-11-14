
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] GameObject loadingUIPrefab;
    GameObject loadingUI;
    TMP_Text loadingProgressText;

    private string nextSceneName;


    public void MoveScene(string sceneName)
    {
        
        nextSceneName = sceneName;
        TransitionScreen.instance.StartingTransition(TransitionPosition.ToBlack, 2f, GoToNextScene);
        SoundsOnSceneManager.instance.AllAudioFadeOut();

        Debug.Log("Moving Scene");
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
