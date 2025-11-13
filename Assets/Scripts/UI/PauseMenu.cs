using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private RectTransform bgPanel;
    [SerializeField] private RectTransform menuPanel;

    public UnityAction onBackPressed;

    public void OnBackToGameButtonClicked()
    {
        onBackPressed?.Invoke();
    }

    [ContextMenu("Show Pause Menu")]
    public void Show()
    {
        gameObject.SetActive(true);
        Sequence s = DOTween.Sequence();
        s.Append(bgPanel.DOLocalMoveX(0, 0.2f).From(new Vector3(1420f, 0, 0)).SetEase(Ease.InSine));
        s.Append(menuPanel.DOLocalMoveX(0, 0.5f).From(new Vector3(1980f, 0f, 0)).SetEase(Ease.OutQuad));
    }

    [ContextMenu("Hide Pause Menu")]
    public void Hide()
    {
        Sequence s = DOTween.Sequence();
        s.Append(menuPanel.DOLocalMoveX(1500, 0.5f).From(new Vector3(0, 0, 0)).SetEase(Ease.InSine));
        s.Append(bgPanel.DOLocalMoveX(1980, 0.5f).From(new Vector3(0, 0, 0)).SetEase(Ease.OutSine));
        s.AppendCallback(() => gameObject.SetActive(false));

    }
}
