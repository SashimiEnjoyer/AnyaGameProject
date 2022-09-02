using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressButton : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    private bool bbutton1;
    private bool bbutton2;
    private bool bbutton3;

    private Button currentbutton;
    private float commontime;

    void Awake()
    {
        currentbutton = button1;
    }

    void Update()
    {
        commontime += Time.unscaledDeltaTime;

        if (commontime >= 5f)
        commontime = 0;

        if (commontime >= 0.1f && bbutton1 == true)
        currentbutton = button1;

        if (commontime >= 0.1f && bbutton2 == true)
        currentbutton = button2;

        if (commontime >= 0.1f && bbutton3 == true)
        currentbutton = button3;
    }

    public void Press()
    {
        currentbutton.onClick.Invoke(); 
    }

    public void Switchto1()
    {
        commontime = 0;

        bbutton1 = true;
        bbutton2 = false;
        bbutton3 = false;
    }

    public void Switchto2()
    {
        commontime = 0;

        bbutton1 = false;
        bbutton2 = true;
        bbutton3 = false;
    }

    public void Switchto3()
    {
        commontime = 0;

        bbutton1 = false;
        bbutton2 = false;
        bbutton3 = true;
    }
}
