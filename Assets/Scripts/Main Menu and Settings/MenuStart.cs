using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStart : MonoBehaviour
{
    public Image highlighted;
    private ButtonFadeOut buttonFadeOut;
    private EventController eventController;


    void Start()
    {
        eventController = GetComponent<EventController>();
        var opacity = highlighted.color;
        opacity.a = 0;
        highlighted.color = opacity;
    }


    public void TaskOnClick()
    {
        Debug.Log("Game Start");
        eventController.EventStart = true;
    }

    public void MouseOver()
    {
        var opacity = highlighted.color;
        opacity.a = 1;
        highlighted.color = opacity;
    }

    public void MouseExit()
    {
        var opacity = highlighted.color;
        opacity.a = 0;
        highlighted.color = opacity;
    }
}
