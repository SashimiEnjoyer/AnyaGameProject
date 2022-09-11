using System;
using UnityEngine;
using DG.Tweening;

 public enum TransitionPosition { FromBlack, ToBlack, Full}

public class TransitionScreen : MonoBehaviour
{
    public static TransitionScreen instance;
    public Action OnFinished;

    public TransitionPosition transitionPos;
    [SerializeField] CanvasGroup canvasGroup;

    float transitionTimer = 1f;

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
                DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, transitionTimer).SetEase(Ease.Linear).OnComplete(() => OnFinished?.Invoke());
                break;
            case TransitionPosition.ToBlack:
                canvasGroup.alpha = 0;
                DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, transitionTimer).SetEase(Ease.OutQuint).OnComplete(() => OnFinished?.Invoke());
                break;
            case TransitionPosition.Full:
                canvasGroup.alpha = 0;
                break;

        }

    }

    private void OnDestroy()
    {
        OnFinished = null;
    }

}
