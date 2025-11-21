using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] HUDManager hudManager;

    public void TriggerPlayerDie()
    {
        hudManager.SetDeathScreen();
    }

}
