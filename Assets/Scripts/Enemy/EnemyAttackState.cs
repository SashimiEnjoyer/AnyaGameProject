using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : CharacterState
{
    public EnemyAttackState(EnemyController enemy) : base(enemy) { }

    float time;

    public override void EnterState()
    {

        time = 0;
    }

    public override void Tick()
    {
        time += Time.deltaTime;

        if (time < 0.1f)
        {

        }

        enemy.EnemyDoAttack();

        if (time > enemy.AttackAnimLength)
        {
            enemy.anim.SetBool("EnemyAttack", false);
            ExitState();
        }
                
    }

    public override void ExitState()
    {
        time = 0;
    }
}
