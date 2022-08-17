using System;
using UnityEngine;

 public enum TransitionPosition { FromBlack, ToBlack, Full}

public class TransitionScreen : MonoBehaviour
{
    public static TransitionScreen instance;
    public Action OnFinished;

    public TransitionPosition transitionPos;
    [SerializeField] CanvasGroup canvasGroup;

    bool isStarting = false;
    float transitionTimer = 1f;
    float counter = 0f;
    bool reverse = false;

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
    public void StartingTransition(TransitionPosition _transitionPos, float _transitionTimer, Action _OnFinished)
    {
        OnFinished = _OnFinished;
        if (InGameTracker.instance != null && InGameTracker.instance.gameState != GameplayState.Stop)
            InGameTracker.instance.gameState = GameplayState.Stop;

        transitionPos = _transitionPos;
        transitionTimer = _transitionTimer;

        switch (_transitionPos)
        {
            case TransitionPosition.FromBlack:
                canvasGroup.alpha = 1f;
                break;
            case TransitionPosition.ToBlack:
            case TransitionPosition.Full:
                canvasGroup.alpha = 0;
                break;
        }

        if (!isStarting)
            isStarting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarting)
            return;

        switch (transitionPos)
        {
            case TransitionPosition.FromBlack:
                
                transitionTimer -= Time.deltaTime;

                canvasGroup.alpha = transitionTimer;

                if (transitionTimer <= 0)
                {
                    isStarting = false;
                    OnFinished?.Invoke();
                    Destroy(gameObject);    // Destroy when done transitioning 
                }
                break;

            case TransitionPosition.ToBlack:
                
                counter += Time.deltaTime;

                canvasGroup.alpha = counter;

                if (counter >= transitionTimer)
                {
                    isStarting = false;
                    OnFinished?.Invoke();
                    //Destroy(this.gameObject);
                }
                break;

            case TransitionPosition.Full:


                if(!reverse)
                    counter += Time.deltaTime;
                else
                    counter -= Time.deltaTime;

                canvasGroup.alpha = counter;

                if(counter >= transitionTimer + 2)
                {
                    reverse = true;
                }

                if(counter <= 0 && reverse)
                {
                    isStarting = false;
                    OnFinished?.Invoke();
                    Destroy(gameObject);
                }

                break;
        }
    }

    private void OnDestroy()
    {
        OnFinished = null;
    }

}
