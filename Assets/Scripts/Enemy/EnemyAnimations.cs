using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator animator;
    private Animation anim;
    private EnemyController enemyController;
    public bool animationisPlaying = false;
    public float commontime = 0f;
    public float limitTime;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    public void Update()
    {
        commontime +=Time.deltaTime;

        if (commontime >= 100)
        commontime = 0f;

        if (animationisPlaying == true && commontime >= limitTime)
        {
            animationisPlaying = false;
            PlayEnemyAnimationIdle();
        }
    }

    public void PlayEnemyAnimationHurt()
    {
        animationisPlaying = true;
        animator.Play("Base Layer.Enemy_Hurt", 0, 0f);
        commontime = 0f;
        limitTime = enemyController.AnimationLength("Enemy_Hurt");
    }

    public void PlayEnemyAnimationIdle()
    {
        animator.Play("Base Layer.Enemy_Idle", 0, 0f);
    }
}

