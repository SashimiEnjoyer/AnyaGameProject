using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTeleportObject : MonoBehaviour
{
    public EventController eventController;
    
    [Header("Objects to Teleport")]
    public GameObject objectTeleport;
    public GameObject objectTeleportLocation;
    public bool teleportTransition = false;

    public void Update()
    {
        if (eventController.EventStart == true)
        {
            objectTeleport.transform.position = objectTeleportLocation.transform.position;
        }
    }
}
