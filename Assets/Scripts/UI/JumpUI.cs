using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpUI : MonoBehaviour
{
    
    [SerializeField] private Image JumpNotReadyImage;
    [SerializeField] PlayerController playerController;



    void Start()
    {

    }


    void Update()
    {
        if(playerController.jumpCounter <= 2)
        JumpNotReadyImage.fillAmount = 1;

        if(playerController.jumpCounter <= 1)
        JumpNotReadyImage.fillAmount = 0.5f;

        if(playerController.jumpCounter <= 0)
        JumpNotReadyImage.fillAmount = 0;
    }
}
