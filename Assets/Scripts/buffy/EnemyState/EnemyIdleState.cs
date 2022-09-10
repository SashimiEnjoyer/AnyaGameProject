using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyStateId GetId()
    {
        return EnemyStateId.Idle;
    }

    public void Enter(Enemy enemy)
    {
        enemy.animator.SetBool("isWalking", false);
        enemy.Stop();
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
