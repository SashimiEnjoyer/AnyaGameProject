using UnityEngine;

public class PatrolEnemy_AggroState : CharacterState
{
    public PatrolEnemy_AggroState(PlayerController _character) : base(_character)
    {
    }

    public override void EnterState()
    {
        enemy.SetAnimatorState(enemy.anim,"Enemy_Attack");
    }

    public override void Tick()
    {

        if (Mathf.Abs(Vector2.Distance(enemy.transform.position, enemy.startingPoint.position)) > enemy.maxDistanceBeforeDie)
        {
            enemy.SetState(enemy.enemyDied);
        }

        if (enemy.CheckMask(enemy.borderMask) && !enemy.Resetting)
        {
            enemy.StopMove();
        }
        else
        {
            enemy.Move();
        }

        enemy.EnemyDoAttack();
    }

    public override void ExitState()
    {
        enemy.Resetting = true;
    }
}
