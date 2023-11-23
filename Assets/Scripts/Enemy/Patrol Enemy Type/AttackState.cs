using UnityEngine;

public static partial class PatrolType
{
    public class AttackState : CharacterState
    {
        float timeToAttack = 0f;
        PatrolTypeEnemy en;

        public AttackState(PatrolTypeEnemy _enemy) : base(_enemy)
        {
            en = _enemy;
        }

        public override void EnterState()
        {
            enemy.SetAnimatorState(enemy.anim, "Enemy_Attack");
            timeToAttack = Time.time + 2f;
        }

        public override void Tick()
        {

            if (Time.time > timeToAttack)
            {
                enemy.SetState(enemy.chaseState);
                timeToAttack = 0f;
            }

            enemy.Move(2f);
            enemy.EnemyDoAttack();
        }
    }
}
