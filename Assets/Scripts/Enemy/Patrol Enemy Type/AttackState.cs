using UnityEngine;

public static partial class PatrolType
{
    public class AttackState : CharacterState
    {
        float timer = 0f;
        public AttackState(EnemyController enemy) : base(enemy)
        {
        }

        public override void EnterState()
        {
            enemy.SetAnimatorState(enemy.anim, "Enemy_Attack");
        }

        public override void Tick()
        {
            timer += Time.deltaTime;

            if (timer >= 2f)
            {
                enemy.SetState(enemy.chaseState);
                timer = 0f;
            }

            enemy.Move(2f);
            enemy.EnemyDoAttack();
        }
    }
}
