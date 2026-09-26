using Animancer;
using UnityEngine;

public static partial class PatrolType
{
    [System.Serializable]
    public class AttackState : CharacterState
    {
        private float timeToAttack = 0f;
        private float preAttack = 0f;
        private float runSpeedMultiplier;
        private EnemyController en;
        private AnimancerState state;

        private int counter;
        private int looping;

        public AttackState(EnemyController _enemy) : base(_enemy)
        {
            en = _enemy;

            en.onEnemyDoAttack += DoAttack;
        }

        public override void EnterState()
        {
            counter = 0;
            looping = 3;

            state = en.AnimancerComponent.Play(en.attackClip);
            state.Events(this).OnEnd ??= OnEnd;

            runSpeedMultiplier = Random.Range(3.9f, 4.3f);

            //en.SetAnimatorState(en.anim, "Enemy_Attack");
            //preAttack = Time.time + en.preAttackTimer;
            //timeToAttack = Time.time + en.attackTimer;
        }

        void OnEnd()
        {
            //looping--;

            if (looping <= 0)
            {
                Debug.LogWarning("enemy Attak Ended");
                en.SetState(en.chaseState);
            }
            else
            {
                state.Time = 0;
                looping -= 1;
                en.AnimancerComponent.Play(en.attackClip);
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
                    
                    if (en.CheckMask(en.borderMask) && !en.Resetting)
                        en.StopMove();
                    else
                    {
                        en.Move(runSpeedMultiplier);
                    }
                    break;

                default:
                    en.Move(runSpeedMultiplier);
                    break;
            }

        }

        public override void ExitState()
        {
            en.attackHitBox.SetActive(false);
        }
    }
}
