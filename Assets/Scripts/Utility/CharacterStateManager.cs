using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    protected CharacterState currState;

    public void SetState(CharacterState state)
    {
        currState?.ExitState();
        currState = state;
        currState?.EnterState();
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
