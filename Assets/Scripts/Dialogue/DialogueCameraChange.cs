using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;

public class DialogueCameraChange : MonoBehaviour
{
    public DialogueController dialogueController;
    public CinemachineVirtualCamera vcam;
    public Transform player;
    public Transform cameraView;
    
    void Update()
    {
        if (dialogueController.isOpen == true)
        {
            vcam.Follow = cameraView;
        }

        if (dialogueController.DialogueEnd == true)
        {
            vcam.Follow = player;
        }
    }
}
