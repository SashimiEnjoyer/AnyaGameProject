using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventIdle : MonoBehaviour
{
    public PlayerEventController playerEventController;
    public EventController eventController;
    private float commontime;
    public float eventdelay;

    public void Update()
    {
        if (eventController.EventStart == true)
        {
            commontime += Time.deltaTime;

            if (commontime >= eventdelay)
            {
                playerEventController.PlayAnimationIdle();
                EventEnd();
            } 
        }
    }

    private void EventEnd()
    {
        Destroy(this);
    }
    
}
