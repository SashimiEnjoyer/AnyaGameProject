
using UnityEngine;
using TMPro;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] GameObject loadingUIPrefab;
    GameObject loadingUI;
    TMP_Text loadingProgressText;

    private string nextSceneName;

    public void MoveScene(string sceneName)
    {
        nextSceneName = sceneName;
        GameManager.instance.SoundsOnSceneManager.AllAudioFadeOut();
        TransitionScreen.instance.StartingTransition(TransitionPosition.ToBlack, 2f, GoToNextScene);

        Debug.Log("Moving Scene");
    }

    void GoToNextScene()
    {
        Debug.Log("Go To Next Scene");
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
