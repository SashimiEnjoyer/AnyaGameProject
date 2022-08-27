using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public bool start = false;
    public Image image;
    private float commontime;
    public string m_Scene;

    void Update()   
    {
        if (start == true)
        {
            var opacity = image.color;
            opacity.a += Time.deltaTime * 2;
            image.color = opacity;
            commontime += Time.deltaTime * 2;

            if (commontime >= 5f)
            {
                SceneMove();
            }
        }
    }

    public void SceneMove()
    {
        SceneLoader.LoadScene(m_Scene);
    }

    public void EventStart()
    {
        start = true;
    }
}
