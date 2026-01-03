using UnityEngine;

public static partial class PatrolType
{
    public class ChaseState : CharacterState
    {
        PatrolTypeEnemy en;
        float interval;
        public ChaseState(PatrolTypeEnemy _enemy) : base(_enemy)
        {
            en = _enemy;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Chase State!");
            baseEnemy.AnimancerComponent.Play(en.walkAnim);
            //baseEnemy.SetAnimatorState(baseEnemy.anim, "Enemy_Walk");
            interval = Time.time + Random.Range(en.chaseToAttackTimeInterval.x, en.chaseToAttackTimeInterval.y);
        }

        public override void Tick()
        {
            if (Time.time > interval)
            {
                if (Mathf.Abs(Vector2.Distance(baseEnemy.transform.position, baseEnemy.playerTransform.position)) < en.chaseToAttackTriggerDistance) 
                {
                    if(baseEnemy.usePreAttack)
                        baseEnemy.SetState(baseEnemy.preAttackState);
                    else
                        baseEnemy.SetState(baseEnemy.attackState);
                }

                else if (Mathf.Abs(baseEnemy.transform.position.x - baseEnemy.playerTransform.position.x) > 35f ||
                        Mathf.Abs(baseEnemy.transform.position.y - baseEnemy.playerTransform.position.y) > 7f)
                {
                    //baseEnemy.transform.position = baseEnemy.startingPoint.position;
                    baseEnemy.SetState(baseEnemy.defaultState);

                }
            }

            switch(en.AggroStatus)
            {
                case EnemyAggroStatus.Semi:
                    
                    if (baseEnemy.CheckMask(baseEnemy.borderMask) && !baseEnemy.Resetting)
                        baseEnemy.StopMove();
                    else
                    {
                        baseEnemy.Move(Random.Range(1.6f, 1.8f));
                    }
                    break;

                default:
                    baseEnemy.Move(Random.Range(1.5f, 1.7f));
                    break;
            }
            

        }

        public override void PhysicTick()
        {
            if (Mathf.Sign(baseEnemy.CurrentDirection) != Mathf.Sign(baseEnemy.PlayerDirection().x))
            {
                baseEnemy.Flip();
            }
        }

    }
}
