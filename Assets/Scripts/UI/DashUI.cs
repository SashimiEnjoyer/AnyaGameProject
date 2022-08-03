using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{ 
    [SerializeField] private Image DashReadyImage;
    [SerializeField] private float dashcooldown;
    [SerializeField] PlayerController playerController;
    private float dashtimeUI;
 
    
    

    public void Start()
    {
        dashcooldown = 0;
    }

    public void Update()
    {
        dashtimeUI = Time.deltaTime;

        if (playerController.isDashing == true)
        {
            dashcooldown = 0.4f;
            dashtimeUI = 0;
        }

        if (dashcooldown <= 0.8f)
        {
            dashcooldown -= dashtimeUI;
            DashReadyImage.fillAmount = dashcooldown * 2.5f;
        }

        if (dashcooldown <= -0.001f)
        {
            dashcooldown = 0;
        }
        
    }   
}
