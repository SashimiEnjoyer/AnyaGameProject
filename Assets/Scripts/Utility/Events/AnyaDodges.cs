using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyaDodges : MonoBehaviour
{
    public EventController eventController;
    private float commontime;
    public GameObject player;

    void Update()
    {

        if (eventController.EventStart == true)
        {
            commontime += Time.deltaTime;

            if (commontime >= 0.1f)
            {
                player.GetComponent<PlayerController>().Flip();
            }

            if (commontime >= 0.3f)
            {
                player.GetComponent<PlayerController>().Dash();
            }
        }
    }
}
