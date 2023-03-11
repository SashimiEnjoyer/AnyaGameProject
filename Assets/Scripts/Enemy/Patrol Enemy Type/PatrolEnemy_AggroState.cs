using UnityEngine;

public class PatrolEnemy_AggroState : CharacterState
{
    public PatrolEnemy_AggroState(EnemyController enemy) : base(enemy)
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

        if (Mathf.Sign(enemy.CurrentDirection) != Mathf.Sign(PlayerPosDir().x))
        {
            enemy.Flip();
        }

        enemy.Move();

        if(Mathf.Abs(Vector2.Distance(enemy.transform.position, enemy.player.position)) < 3)
            enemy.EnemyDoAttack();
    }

    Vector2 PlayerPosDir()
    {
        return (enemy.player.position - enemy.transform.position).normalized;
    }

    public override void ExitState()
    {
        enemy.Resetting = true;
    }
}
