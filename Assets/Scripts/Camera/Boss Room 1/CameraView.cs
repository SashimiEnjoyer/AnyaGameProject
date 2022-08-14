using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;

public class CameraView : MonoBehaviour
{
    public CinemachineCameraChange cccam;
    public bool onPlayer;
    public bool onBossRoom;
    public bool onObjectRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cccam.player = onPlayer;
            cccam.bossRoom = onBossRoom;
            cccam.object1Room = onObjectRoom;
        }
    }
}
