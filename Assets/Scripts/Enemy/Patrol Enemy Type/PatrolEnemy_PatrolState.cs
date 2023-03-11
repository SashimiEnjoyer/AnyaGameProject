
using System.Collections;
using UnityEngine;

public class PatrolEnemy_PatrolState : CharacterState
{
    public PatrolEnemy_PatrolState(EnemyController enemy) : base(enemy)
    {
    }

    public override void EnterState()
    {
        //enemy.onTouchBorder += Flip;
        enemy.SetAnimatorState(enemy.anim,"Enemy_Walk");
    }

    public override void Tick()
    {

        if (Mathf.Abs(Vector2.Distance(enemy.transform.position, enemy.startingPoint.position)) > enemy.maxDistanceBeforeDie)
        {
            enemy.SetState(enemy.enemyDied);
        }

        if (enemy.CheckMask(enemy.borderMask) && !enemy.Resetting)
            enemy.Flip();
        else
            enemy.Move();

    }

    public override void ExitState()
    {
        //enemy.onTouchBorder -= Flip;
    }

}
