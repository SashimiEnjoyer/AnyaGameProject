using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    protected CharacterState currState;
    protected string currentAnimationLayer;

    public void SetState(CharacterState state)
    {
        currState?.ExitState();
        currState = state;
        currState?.EnterState();
    }

    public void SetAnimatorState(Animator animator, string animationStateName, bool canMultiple = false)
    {
        if (string.Equals(currentAnimationLayer, animationStateName) && !canMultiple)
            return;

        currentAnimationLayer = animationStateName;

        animator.Play(animationStateName, 0, 0);
    }

    protected virtual void Update()
    {
        if(currState != null)
            currState.Tick();
    }

    protected virtual void FixedUpdate()
    {
        if (currState != null)
            currState.PhysicTick();
    }

}
