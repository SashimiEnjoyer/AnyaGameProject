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
            baseEnemy.SetAnimatorState(baseEnemy.anim, "Enemy_Attack");
            en.attackHitBox.SetActive(true);
            timeToAttack = Time.time + 2f;
        }

        public override void Tick()
        {

            if (Time.time > timeToAttack)
            {
                baseEnemy.SetState(baseEnemy.chaseState);
                timeToAttack = 0f;
            }

            baseEnemy.Move(2.3f);
            
        }

        public override void ExitState()
        {
            en.attackHitBox.SetActive(false);
        }
    }
}
