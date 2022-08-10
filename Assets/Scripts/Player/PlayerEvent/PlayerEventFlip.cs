using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventFlip : MonoBehaviour
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

            if (commontime >= eventdelay)
            {
                playerController.Flip();
                EventEnd();
            } 
        }
    }

    private void EventEnd()
    {
        Destroy(this);
    }
    
}
