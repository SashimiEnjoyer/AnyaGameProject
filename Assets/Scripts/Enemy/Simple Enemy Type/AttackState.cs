
using UnityEngine;

public static partial class SimpleEnemy
{
    public class AttackState : CharacterState
    {
        EnemyController currEnemy;
        float interval;

        public AttackState(EnemyController _enemy) : base(_enemy)
        {
            currEnemy = _enemy;
        }

        public override void Tick()
        {
            interval = Time.time + currEnemy.patrolTimeRange.x;
            if (Time.time >= interval)
            {
                currEnemy.SetState(currEnemy.enemyDied);
            }
            else
            {
                currEnemy.Move(4);
            }

        }

        public override void ExitState()
        {
            currEnemy.rb.linearVelocity = Vector2.zero;
        }
    }
}
