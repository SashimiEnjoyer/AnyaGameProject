using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroState : EnemyState
{

    public EnemyStateId GetId()
    {
        return EnemyStateId.Aggro;
    }

    public void Enter(Enemy enemy)
    {
        enemy.animator.SetBool("isWalking", true);
    }

    public void FixedUpdate(Enemy enemy)
    {

    }


    public void Update(Enemy enemy)
    {
        if ((Mathf.Sign(enemy.movementSpeed) != Mathf.Sign(enemy.getPlayerDirection().x)))
        {
            enemy.Flip();
        }
        if (enemy.isCollideWithWall())
        {
            enemy.Jump();
        }
        enemy.Move();
    }

    public void Exit(Enemy enemy)
    {
    }
}
