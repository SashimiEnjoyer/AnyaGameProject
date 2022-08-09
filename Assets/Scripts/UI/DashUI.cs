using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] PlayerController playerController;


    public void Update()
    {
        slider.value = (playerController.dashCounter  / (playerController.dashTime + playerController.dashCooldown)) * 100;
    }   

}
