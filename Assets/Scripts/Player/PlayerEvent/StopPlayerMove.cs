using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerMove : MonoBehaviour
{
    public GameObject player;
    PlayerEventController playerEventController;
    Animator animator;

    void Start()
    {
        playerEventController = player.GetComponent<PlayerEventController>();
        animator = player.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (player.transform.position.x >= transform.position.x)
        {
            playerEventController.horizontalInput = 0f;
            animator.SetFloat("Speed",0);
        }
        
    }
}
