using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCutScene : MonoBehaviour
{
    public PlayerEventController playerEventController;
    public EventController eventController;
    public Animator animator;
    private float commontime;
    public float eventdelay1;
    public float eventdelay2;
    public float eventdelay3;
    public float eventenddelay;
    public float direction;

    void Update()
    {
        if (eventController.EventStart == true)
        {
            commontime += Time.deltaTime;

            if (commontime >= eventdelay1 && commontime <= eventdelay1 + 0.1f)
            {
                playerEventController.PlayAnimationWakeUp();
            } 

            if (commontime >= eventdelay2 && commontime <= eventdelay2 + 0.3f)
            {
                //playerEventController.PlayAnimationIdle();
            } 

            if (commontime >= eventdelay3 && commontime <= eventdelay3 + 0.1f)
            {
                playerEventController.horizontalInput = 1f * direction;
                animator.SetFloat("Speed",1);
            } 

            if (commontime >= eventenddelay)
            {
                EventEnd();
            } 
        }
    }

    private void EventEnd()
    {
        Destroy(this);
    }
}
