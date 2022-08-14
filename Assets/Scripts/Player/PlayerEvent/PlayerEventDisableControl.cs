using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventDisableControl : MonoBehaviour
{
    public PlayerController playerController;
    public EventController eventController;
    private float commontime;
    public float eventdelay;

    public void Update()
    {
        if (eventController.EventStart == true)
        {
            commontime += Time.deltaTime;
            playerController.keyboardInput = false;

            if (commontime >= eventdelay)
            { 
                EventEnd();
            } 
        }
    }

    private void EventEnd()
    {
        playerController.keyboardInput = true;
        Destroy(this);
    }
    
}
