using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class EnemyMassageBox : InteractableObject, IInteractable
{
    [SerializeField] UnityEvent EventsExecuted;

    public void ExecuteInteractable()
    {
        EventsExecuted?.Invoke();
    }

}
