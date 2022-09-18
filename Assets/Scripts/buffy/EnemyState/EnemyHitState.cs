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
        enemy.animator.SetTrigger("Hurt");
        enemy.rb.AddForce(new Vector2(enemy.hitDirection.x > enemy.transform.position.x ? -100 : 100, 100), ForceMode2D.Force);
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
