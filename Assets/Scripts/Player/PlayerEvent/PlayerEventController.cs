using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEventController : MonoBehaviour
{
    public float horizontalInput = 0f;
    public float speed;
    private Animator animator;
    private PlayerEventController playerEventController;
    public bool animationisPlaying = false;
    private float commontime = 0f;
    private float limitTime;
    public bool isFacingRight = true;
    public Rigidbody2D rb;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        playerEventController = GetComponent<PlayerEventController>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, rb.velocity.y);
        commontime +=Time.deltaTime;

        if (commontime >= 100)
        commontime = 0f;

        if (animationisPlaying == true && commontime >= limitTime)
        {
            animationisPlaying = false;
            //PlayAnimationIdle();
        }

        if (!isFacingRight && horizontalInput > 0)
                Flip();
        if (isFacingRight && horizontalInput < 0)
                Flip();
    }

    public void PlayAnimation(string animationName)
    {
        animationisPlaying = true;
        animator.Play("Base Layer." + animationName, 0, 0f);
        commontime = 0f;
        limitTime = playerEventController.AnimationLength(animationName);
    }
    
    public void PlayAnimationAttack()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Anya_Attack1", 0, 0f);
        commontime = 0f;
        limitTime = playerEventController.AnimationLength("Anya_Attack1");
    }
    
    public void PlayAnimationJumpUp()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Anya_JumpUp", 0, 0f);
        commontime = 0f;
        limitTime = playerEventController.AnimationLength("Anya_JumpUp");
    }

    public void PlayAnimationDash()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Anya_Dash", 0, 0f);
        commontime = 0f;
        limitTime = playerEventController.AnimationLength("Anya_Dash");
    }

    public void PlayAnimationHurt()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Anya_Hurt", 0, 0f);
        commontime = 0f;
        limitTime = playerEventController.AnimationLength("Anya_Hurt");
    }

    public void PlayAnimationDied()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Anya_Hurt", 0, 0f);
        commontime = 0f;
        limitTime = playerEventController.AnimationLength("Anya_Hurt");
    }

    public void PlayAnimationIdle()
    {
        animator.Play("Base Layer.Anya_Idle", 0, 0f);
    }

    public void PlayAnimationJumpGround()
    {
        if (animationisPlaying == false)
        commontime = 0f;

        animationisPlaying = true;
        animator.Play("Base Layer.Anya_JumpGround", 0, 0f);
        limitTime = playerEventController.AnimationLength("Anya_JumpGround");
    }

    public void PlayAnimationWakeUp()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Anya_WakeUp", 0, 0f);
        commontime = 0f;
        limitTime = playerEventController.AnimationLength("Anya_WakeUp");
    }

    public void PlayAnimationSleep()
    {
        animator.Play("Base Layer.Anya_Sleep", 0, 0f);
    }

    public float AnimationLength(string animationName)
    {
        RuntimeAnimatorController ra = animator.runtimeAnimatorController;

        for (int i = 0; i < ra.animationClips.Length; i++)
        {
            if(ra.animationClips[i].name == animationName)
            {
                return ra.animationClips[i].length;
            }
        }
        Debug.LogError("Animation Name not found!");
        return 0;
    }

    public void Flip()
    {
        if (isFacingRight)
        {
            isFacingRight = false;
            transform.Rotate(0, 180f, 0);
        }
        else
        {
            isFacingRight = true;
            transform.Rotate(0, 180f, 0);
        }
    }
}
