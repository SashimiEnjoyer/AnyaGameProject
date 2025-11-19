using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] HUDManager hudManager;

    void Start()
    {
        InGameTracker.instance.onLoseEnding += TriggerPlayerDie;
    }

    void OnDestroy()
    {
        InGameTracker.instance.onLoseEnding -= TriggerPlayerDie;
    }

    public void TriggerPlayerDie()
    {
        hudManager.SetDeathScreen();
    }

}
