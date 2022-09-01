using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAwakePlayAnimation : MonoBehaviour
{
    [Header("Custom Event")] public UnityEvent onInteracting;

    void Start()
    {
        ExecuteInteractable();
    }

    protected void ExecuteInteractableEvent()
    {
        onInteracting?.Invoke();
    }

    public void ExecuteInteractable()
    {
        ExecuteInteractableEvent();
    }
}
