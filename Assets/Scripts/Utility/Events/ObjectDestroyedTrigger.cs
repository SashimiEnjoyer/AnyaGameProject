using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyedTrigger : MonoBehaviour
{
    public GameObject TobeDestroyed;
    public EventController eventController;

    public void Update()
    {
        if (TobeDestroyed == null)
        {
            eventController.EventStart = true;
        }
    }
}
