using System;
using System.Collections;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    protected CharacterState currState;
    protected CharacterState prevState;
    protected string currentAnimationLayer;
    private IEnumerator coroutine;
    private WaitForSeconds wait;
    private bool isStop = false;

    public void SetState(CharacterState state)
    {
        currState?.ExitState();
        currState = state;
        currState?.EnterState();
    }

    public void SetAnimatorState(Animator animator, string animationStateName, bool canMultiple = false)
    {
        if (animator == null)
            return;

        if (string.Equals(currentAnimationLayer, animationStateName) && !canMultiple)
            return;

        currentAnimationLayer = animationStateName;

        animator.Play(animationStateName, 0, 0);
    }

    public void FunctionWithInterval(Action action, float interval)
    {
        if(coroutine == null)
        {
            coroutine = CoroutineFunc(action);
            wait = new WaitForSeconds(interval);
        }
        StartCoroutine(coroutine);
    }

    private IEnumerator CoroutineFunc(Action action)
    {
        while (true)
        {
            yield return wait;
            action?.Invoke();
        }
    }

    protected virtual void Update()
    {
        if (currState != null)
            currState.Tick();
        
    }

    protected virtual void FixedUpdate()
    {
        if (currState != null)
            currState.PhysicTick();
    }

}
