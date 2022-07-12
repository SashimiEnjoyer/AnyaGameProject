using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyAttackState : CharacterState
{
    public PatrolEnemyAttackState(EnemyController enemy) : base(enemy) { }

    public override void Tick()
    {
        enemy.Move();
        enemy.EnemyAttacking();
    }
}
