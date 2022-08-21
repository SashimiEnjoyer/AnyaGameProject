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

    public void SetAnimatortate(Animator animator, string animationName, bool canMultiple = false)
    {
        if (string.Equals(currentAnimationLayer, animationName) && !canMultiple)
            return;

        currentAnimationLayer = animationName;

        animator.Play(animationName, 0, 0);
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
