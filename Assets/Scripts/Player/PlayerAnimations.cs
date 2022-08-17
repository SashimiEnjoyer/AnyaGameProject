using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private Animation anim;
    private PlayerController playerController;
    public bool animationisPlaying = false;
    public float commontime = 0f;
    public float limitTime;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    public void Update()
    {

        commontime +=Time.deltaTime;

        if (commontime >= 100)
        commontime = 0f;

        if (animationisPlaying == true && commontime >= limitTime)
        {
            animationisPlaying = false;
            PlayAnimationIdle();
        }
    }
    
    public void PlayAnimationAttack()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Anya_Attack1", 0, 0f);
        commontime = 0f;
        limitTime = playerController.AnimationLength("Anya_Attack1");
    }
    
    public void PlayAnimationJumpUp()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Anya_JumpUp", 0, 0f);
        commontime = 0f;
        limitTime = playerController.AnimationLength("Anya_JumpUp");
    }

    public void PlayAnimationDash()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Anya_Dash", 0, 0f);
        commontime = 0f;
        limitTime = playerController.AnimationLength("Anya_Dash");
    }

    public void PlayAnimationHurt()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Anya_Hurt", 0, 0f);
        commontime = 0f;
        limitTime = playerController.AnimationLength("Anya_Hurt");
    }

    public void PlayAnimationDied()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Anya_Hurt", 0, 0f);
        commontime = 0f;
        limitTime = playerController.AnimationLength("Anya_Hurt");
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
        limitTime = playerController.AnimationLength("Anya_JumpGround");
    }

}
