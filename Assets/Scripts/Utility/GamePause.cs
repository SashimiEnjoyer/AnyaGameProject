using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    private bool isPause = false;

    public void Pause()
    {
        Time.timeScale = 0f; 
    }

    public void Unpause()
    {
        Time.timeScale = 1f; 
    }

    public void Update()
    {     
        if (Input.GetKeyDown(KeyCode.O))
        {
            Pause();
            isPause = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Unpause();
            isPause = false;
        }
    }
}
