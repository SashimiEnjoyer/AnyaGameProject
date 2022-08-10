using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;

public class CameraView : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public Transform cameraView;
    public float zoom;
    public GameObject objectActivate;
    public GameObject objectDeactivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CinemachineFramingTransposer tcam = vcam.AddCinemachineComponent<CinemachineFramingTransposer>();

        if (collision.CompareTag("Player"))
        {
            vcam.Follow = cameraView;

            if(zoom != 0f)
            tcam.m_CameraDistance = zoom;

            objectActivate.SetActive(true);
            objectDeactivate.SetActive(false);
        }
    }
}
