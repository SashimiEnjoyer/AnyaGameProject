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
    }

    public void ActivePlayerImpulse()
    {
        playerImpulse.GenerateImpulse();
    }
}
