using System;
using UnityEngine;

 public enum TransitionPosition { Start, End}

public class TransitionScreen : MonoBehaviour
{
    public static TransitionScreen instance;
    public Action OnFinishedStartTransition;
    public Action OnFinishedEndTransition;

    public TransitionPosition transitionPos;
    [SerializeField] CanvasGroup canvasGroup;

    bool isStarting = false;
    float transitionTimer = 1f;
    float counter = 0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    /// <summary>
    /// Function for start the trasition
    /// </summary>
    /// <param name="_transitionPos"> Type of transition</param>
    /// <param name="_transitionTimer"> How long transition last</param>
    public void StartingTransition(TransitionPosition _transitionPos, float _transitionTimer)
    {
        if (!isStarting)
            isStarting = true;

        if (InGameTracker.instance != null && !InGameTracker.instance.isPause)
            InGameTracker.instance.isPause = true;

        transitionPos = _transitionPos;
        transitionTimer = _transitionTimer;

        switch (_transitionPos)
        {
            case TransitionPosition.Start:
                canvasGroup.alpha = 1f;
                break;
            case TransitionPosition.End:
                canvasGroup.alpha = 0;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarting)
            return;

        switch (transitionPos)
        {
            case TransitionPosition.Start:
                
                transitionTimer -= Time.deltaTime;

                canvasGroup.alpha = transitionTimer;

                if (transitionTimer <= 0)
                {
                    isStarting = false;
                    OnFinishedStartTransition?.Invoke();
                    InGameTracker.instance.isPause = false;
                    //gameObject.SetActive(false);
                    Destroy(gameObject);    // Destroy when done transitioning 
                }
                break;

            case TransitionPosition.End:
                
                counter += Time.deltaTime;

                canvasGroup.alpha = counter;

                if (counter >= transitionTimer)
                {
                    isStarting = false;
                    OnFinishedEndTransition?.Invoke();
                    InGameTracker.instance.isPause = false;
                    //Destroy(this.gameObject);
                }
                break;
        }
    }

}
