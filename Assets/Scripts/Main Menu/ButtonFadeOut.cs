using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFadeOut : MonoBehaviour
{
    public bool start = false;
    public Image image;

    void Update()   
    {
        if (start == true)
        {
            image = GetComponent<Image>();
            var opacity = image.color;
            opacity.a -= Time.deltaTime * 2;
            image.color = opacity;
        }
    }
}
