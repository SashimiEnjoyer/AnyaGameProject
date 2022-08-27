using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventWakeUp : MonoBehaviour
{
    public PlayerAnimations playerAnimations;
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
                playerAnimations.PlayAnimationWakeUp();
                EventEnd();
            } 
        }
    }

    private void EventEnd()
    {
        Destroy(this);
    }
    
}
