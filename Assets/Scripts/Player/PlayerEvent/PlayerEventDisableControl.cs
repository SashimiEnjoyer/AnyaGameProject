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

            if (commontime >= eventdelay)
            {
                //InputSystem.DisableDevice(Keyboard.current);
                EventEnd();
            } 
        }
    }

    private void EventEnd()
    {
        //InputSystem.EnableDevice(Keyboard.current);
        Destroy(this);
    }
    
}
