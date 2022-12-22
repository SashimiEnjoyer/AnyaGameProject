using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy_PatrolState : CharacterState
{
    public PatrolEnemy_PatrolState(EnemyController enemy) : base(enemy)
    {
    }

    public override void EnterState()
    {
        enemy.onTouchBorder += Flip;
        enemy.SetAnimatorState(enemy.anim,"Enemy_Walk");
    }

    public override void Tick()
    {
        enemy.move();
        enemy.EnemyDoAttack();
    }

    public override void ExitState()
    {
        enemy.onTouchBorder -= Flip;

    }

    void Flip()
    {
        enemy.Flip();
    }
}
