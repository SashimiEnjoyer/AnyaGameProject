
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

        enemy.EnemyDoAttack();

        if (enemy.CheckMask(enemy.borderMask) && !enemy.Resetting)
        {
            enemy.Flip();
            Debug.Log("Cliff!");
        }else
        {
            enemy.Move();
            Debug.Log("Ground!");
        }
    }

    public override void ExitState()
    {
        //enemy.onTouchBorder -= Flip;
    }

}
