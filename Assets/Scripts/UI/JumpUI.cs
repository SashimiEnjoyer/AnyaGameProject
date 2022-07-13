using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpUI : MonoBehaviour
{
    [SerializeField] private Image JumpNotReadyImage;
    [SerializeField] private int JumpCounter;
    private PlayerController playerController;

    void Start()
    {
        GameObject.Find("Player").GetComponent<PlayerController>();
    }


    void Update()
    {




    }
}
