using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] GameObject transitionScreenPrefab;
    [SerializeField] AudioSource mainBGM;
    [SerializeField] UnityEvent StartSceneEvent;

    private string nextSceneName;

    private void Start()
    {
        GameObject go = Instantiate(transitionScreenPrefab);
        TransitionScreen.instance.OnFinishedStartTransition += FinishedTransition;
        TransitionScreen.instance.StartingTransition(TransitionPosition.Start, 3f);
    }

    [ContextMenu("Test Move Scene")]
    public void StartMoveScene(string sceneName)
    {
        nextSceneName = sceneName;
        GameObject go = Instantiate(transitionScreenPrefab);
        TransitionScreen.instance.OnFinishedEndTransition += GoToNextScene;
        TransitionScreen.instance.StartingTransition(TransitionPosition.End, 5f);
        SoundsOnSceneManager.instance.AllAudioFadeOut();
    }

    void FinishedTransition()
    {

        if(mainBGM != null)
        mainBGM.Play();

        StartSceneEvent?.Invoke();
    }

    void GoToNextScene()
    {
        SceneManager.LoadSceneAsync(nextSceneName);
    }

    private void OnDestroy()
    {
        TransitionScreen.instance.OnFinishedEndTransition -= FinishedTransition;
        TransitionScreen.instance.OnFinishedEndTransition -= GoToNextScene;
    }
}
