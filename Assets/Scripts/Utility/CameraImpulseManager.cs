using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraImpulseManager : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource playerImpulse;
    [SerializeField] private CinemachineImpulseSource enemyImpulse;

    public static CameraImpulseManager instance;

    private void Awake()
    {
        instance = this;
    }

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
