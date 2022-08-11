using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;

public class CinemachineCameraChange : MonoBehaviour
{
    private Animator animator;
    public bool player = true;
    public bool bossRoom = false;
    public bool object1Room = false;
    //private CinemachineFramingTransposer tcam;

    //[Header("Player")]
    //public float zoom1;
    //public CinemachineVirtualCamera vcamPlayer;

    //[Header("Boss Room")]
    //public float zoom2;
    //public CinemachineVirtualCamera vcamBossRoom;

    //[Header("Object 1")]
    //public float zoom3;
    //public CinemachineVirtualCamera vcamObjectRoom;

    void Awake()
    {
        animator = GetComponent<Animator>();  
        //CinemachineFramingTransposer tcamPlayer = vcamPlayer.AddCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Update()
    {
        if (player == true)
        animator.Play("Player");
        //tcam.m_CameraDistance = zoom1;

        if (bossRoom == true)
        animator.Play("BossRoom");
        //tcam.m_CameraDistance = zoom2;

        if (object1Room == true)
        animator.Play("ObjectRoom");
        //tcam.m_CameraDistance = zoom3;
    }
}
