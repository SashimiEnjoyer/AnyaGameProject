
using UnityEngine;

public static partial class SimpleEnemy
{
    public class AttackState : CharacterState
    {
        private EnemyController currEnemy;
        private float interval;
        private float runSpeedMultiplier;

        public AttackState(EnemyController _enemy) : base(_enemy)
        {
            currEnemy = _enemy;
        }

        public override void EnterState()
        {
            runSpeedMultiplier = Random.Range(3.9f, 4.3f);
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
                currEnemy.Move(runSpeedMultiplier);
            }

        }

        public override void ExitState()
        {
            currEnemy.rb.linearVelocity = Vector2.zero;
        }
    }
}
