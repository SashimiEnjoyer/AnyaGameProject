using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSetting : MonoBehaviour
{
    public Image highlighted;
    public bool isActive;

    void Start()
    {
        var opacity = highlighted.color;
        opacity.a = 0;
        highlighted.color = opacity;
    }
    
    void Update()
    {
        if (isActive == true)
        {
            MouseOver();
        }
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

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
