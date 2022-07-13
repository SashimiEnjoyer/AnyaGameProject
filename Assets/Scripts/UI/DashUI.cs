using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{ 
    [SerializeField] private Image DashReadyImage;
    [SerializeField] private float dashcooldown;
    private float dashtime;
 
    
    

    public void Start()
    {
        dashcooldown = 0;
    }

    public void Update()
    {
        dashtime = Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.C) && dashcooldown <= 0)
        {
            dashcooldown = 0.8f;
            dashtime = 0;
        }

        if (dashcooldown <= 0.8f)
        {
            dashcooldown -= dashtime;
            DashReadyImage.fillAmount = dashcooldown * 1.25f;

            

        }
        
    }   
}
