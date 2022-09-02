using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SettingsKey : MonoBehaviour
{
    [Header("Custom Event")] public UnityEvent onInteracting;
    public string keyCode;
   
    protected void ExecuteInteractableEvent()
    {
        onInteracting?.Invoke();
    }

    public void ExecuteInteractable()
    {
        ExecuteInteractableEvent();
    }

    void Update()
    {
        if (Input.GetKey(keyCode))
        ExecuteInteractableEvent(); 
    }
}
