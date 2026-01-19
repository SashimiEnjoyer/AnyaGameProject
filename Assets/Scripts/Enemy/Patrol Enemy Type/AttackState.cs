using Animancer;
using UnityEngine;

public static partial class PatrolType
{
    [System.Serializable]
    public class AttackState : CharacterState
    {

        float timeToAttack = 0f;
        float preAttack = 0f;
        EnemyController en;
        AnimancerState state;

        int counter;
        int looping;

        public AttackState(EnemyController _enemy) : base(_enemy)
        {
            en = _enemy;

            en.onEnemyDoAttack += DoAttack;
        }

        public override void EnterState()
        {
            counter = 0;
            looping = 3;

            state = baseEnemy.AnimancerComponent.Play(en.attackClip);
            state.Events(this).OnEnd ??= OnEnd;  
          
            //baseEnemy.SetAnimatorState(baseEnemy.anim, "Enemy_Attack");
            //preAttack = Time.time + en.preAttackTimer;
            //timeToAttack = Time.time + en.attackTimer;
        }

        void OnEnd()
        {
            //looping--;

            if (looping <= 0)
            {
                Debug.LogWarning("enemy Attak Ended");
                baseEnemy.SetState(baseEnemy.chaseState);
            }
            else
            {
                state.Time = 0;
                looping -= 1;
                baseEnemy.AnimancerComponent.Play(en.attackClip);
            }
        }

        private void DoAttack(bool state)
        {
            en.attackHitBox.SetActive(state);
        }

        public override void Tick()
        {
            switch(en.AggroStatus)
            {
                case EnemyAggroStatus.Semi:
                    
                    if (baseEnemy.CheckMask(baseEnemy.borderMask) && !baseEnemy.Resetting)
                        baseEnemy.StopMove();
                    else
                    {
                        baseEnemy.Move(Random.Range(1.7f, 2.3f));
                    }
                    break;

                default:
                    baseEnemy.Move(Random.Range(1.5f, 2f));
                    break;
            }

        }

        public override void ExitState()
        {
            en.attackHitBox.SetActive(false);
        }
    }
}
