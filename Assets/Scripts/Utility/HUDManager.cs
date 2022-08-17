using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Slider dashSlider;
    [SerializeField] Image HPImage;
    [SerializeField] PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        dashSlider.value = (playerController.dashCounter / (playerController.dashTime + playerController.dashCooldown)) * 100;
        HPImage.fillAmount = PlayerStats.instance.playerHealth / 3;
    }
}
