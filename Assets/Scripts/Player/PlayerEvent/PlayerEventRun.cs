using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventRun : MonoBehaviour
{
    public PlayerEventController playerEventController;
    public EventController eventController;
    public Animator animator;
    private float commontime;
    public float eventdelay;
    public float eventenddelay;
    public float direction;

    public void Update()
    {
        if (eventController.EventStart == true)
        {
            commontime += Time.deltaTime;

            if (commontime >= eventdelay)
            {
                playerEventController.horizontalInput = 1f * direction;
                animator.SetFloat("Speed",1);
                
                if (commontime >= eventenddelay)
                    {
                        EventEnd();
                    } 
            } 
        }
    }

    private void EventEnd()
    {
        Destroy(this);
    }
    
}
