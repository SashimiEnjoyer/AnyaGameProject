using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{

    public EnemyStateId GetId()
    {
        return EnemyStateId.Hit;
    }

    public void Enter(Enemy enemy)
    {
        Debug.Log("Hit!");
        enemy.animator.SetTrigger("Hurt");
        enemy.stateMachine.ChangeState(EnemyStateId.Idle);
    }

    public void FixedUpdate(Enemy enemy)
    {
    }


    public void Update(Enemy enemy)
    {
    }

    public void Exit(Enemy enemy)
    {
    }
}
