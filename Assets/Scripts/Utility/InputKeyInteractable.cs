using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyInteractable : InteractableObject
{
    public string keyCode;

    void Update()
    {
        if (Input.GetKey(keyCode))
        ExecuteInteractableEvent(); 
    }
}
