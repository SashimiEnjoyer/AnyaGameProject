
using UnityEngine;

public static partial class SimpleEnemy
{
    public class AttackState : CharacterState
    {
        SimpleEnemyType currEnemy;
        float interval;

        public AttackState(SimpleEnemyType _enemy) : base(_enemy)
        {
            currEnemy = _enemy;
            interval = Time.time + currEnemy.liveTime;
        }

        public override void EnterState()
        {
            if (currEnemy.isWalkRight && baseEnemy.CurrentDirection < 0)
                baseEnemy.Flip();
            else if(!currEnemy.isWalkRight && baseEnemy.CurrentDirection > 0)
                baseEnemy.Flip();
        }

        public override void Tick()
        {
            baseEnemy.Move(4);

            if (Time.time > interval)
                currEnemy.EnemyHurted();
        }

        public override void ExitState()
        {
            baseEnemy.rb.linearVelocity = Vector2.zero;
        }
    }
}
