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
       Debug.Log("IDLE");
       enemy.animator.SetBool("isWalking", false);
    }

    public void FixedUpdate(Enemy enemy){

    }

    public void Update(Enemy enemy)
    {
    }

    public void Exit(Enemy enemy)
    {
    }
}
