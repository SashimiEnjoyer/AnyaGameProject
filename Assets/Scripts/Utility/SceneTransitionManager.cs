using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] string NextSceneName;
    [SerializeField] GameObject transitionScreenPrefab;
    [SerializeField] AudioSource mainBGM;

    private void Start()
    {
        GameObject go = Instantiate(transitionScreenPrefab);
        TransitionScreen.instance.OnFinishedStartTransition += StartScene;
        TransitionScreen.instance.StartingTransition(TransitionPosition.Start, 3f);
    }

    [ContextMenu("Test Move Scene")]
    public void StartMoveScene()
    {
        GameObject go = Instantiate(transitionScreenPrefab);
        TransitionScreen.instance.OnFinishedEndTransition += GoToNextScene;
        TransitionScreen.instance.StartingTransition(TransitionPosition.End, 5f);
        SoundsOnSceneManager.instance.AllAudioFadeOut();
    }

    void StartScene()
    {

        if(mainBGM != null)
        mainBGM.Play();
    }

    void GoToNextScene()
    {
        SceneManager.LoadSceneAsync(NextSceneName);
    }

    private void OnDestroy()
    {
        TransitionScreen.instance.OnFinishedEndTransition -= StartScene;
        TransitionScreen.instance.OnFinishedEndTransition -= GoToNextScene;
    }
}
