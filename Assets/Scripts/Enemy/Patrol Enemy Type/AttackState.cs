using UnityEngine;

public static partial class PatrolType
{
    public class AttackState : CharacterState
    {
        float timeToAttack = 0f;
        float preAttack = 0f;
        PatrolTypeEnemy en;

        public AttackState(PatrolTypeEnemy _enemy) : base(_enemy)
        {
            en = _enemy;
        }

        public override void EnterState()
        {
            baseEnemy.SetAnimatorState(baseEnemy.anim, "Enemy_Attack");
            preAttack = Time.time + en.preAttackTimer;
            timeToAttack = Time.time + en.attackTimer;
        }

        public override void Tick()
        {
            if(Time.time > preAttack)
                en.attackHitBox.SetActive(true);

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
