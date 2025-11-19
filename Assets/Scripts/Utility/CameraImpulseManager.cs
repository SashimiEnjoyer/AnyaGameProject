using Cinemachine;
using UnityEngine;

public class CameraImpulseManager : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource playerImpulse;
    [SerializeField] private CinemachineImpulseSource enemyImpulse;

    public void ActiveEnemyImpulse()
    {
        enemyImpulse.GenerateImpulse();
        Debug.LogWarning("Enemy Impulse Active");
    }

    public void ActivePlayerImpulse()
    {
        playerImpulse.GenerateImpulse();
        Debug.LogWarning("Player Impulse Active");
    }
}
