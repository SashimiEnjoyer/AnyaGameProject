using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : CharacterState
{
    public EnemyAttackState(EnemyController enemy) : base(enemy) { }

    public override void Tick()
    {
        enemy.EnemyAttacking();
    }
}
