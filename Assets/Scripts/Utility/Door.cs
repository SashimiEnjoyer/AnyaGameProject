using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Door : InteractableObject, IInteractable
{
    [Header ("Door Settings")]
    [SerializeField] string nextScene;
    [SerializeField] SceneTransitionManager transitionManager;

    public void ExecuteInteractable()
    {
        transitionManager.StartMoveScene(nextScene);
    }

}
