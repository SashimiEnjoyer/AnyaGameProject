
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

            if (Time.time >= interval)
            {
                baseEnemy.Flip();
                interval = Time.time + Random.Range(currEnemy.patrolTimeRange.x, currEnemy.patrolTimeRange.y);
            }
            else
            {

                if (baseEnemy.CheckMask(baseEnemy.borderMask) && !baseEnemy.Resetting)
                    baseEnemy.StopMove();
                else
                {
                    baseEnemy.Move(1f);
                }
            }

            currEnemy.Move(4);
        }

        public override void ExitState()
        {
            currEnemy.rb.linearVelocity = Vector2.zero;
        }
    }
}
