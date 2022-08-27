using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStart : MonoBehaviour
{
    public Button button;
    private ButtonFadeOut buttonFadeOut;
    private EventController eventController;

    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
        buttonFadeOut = GetComponent<ButtonFadeOut>();
        eventController = GetComponent<EventController>();
    }


    void TaskOnClick()
    {
        Debug.Log("Game Start");
        buttonFadeOut.start = true;
        eventController.EventStart = true;
    }
}
