using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventRun : MonoBehaviour
{
    public PlayerController playerController;
    public EventController eventController;
    public Animator anim;
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
                playerController.horizontalInput = 1f * direction;
                anim.SetFloat("Speed",1);
                
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
