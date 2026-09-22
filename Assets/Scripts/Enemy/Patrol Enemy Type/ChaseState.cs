using UnityEngine;

public static partial class PatrolType
{
    public class ChaseState : CharacterState
    {
        private EnemyController en;
        private float interval;
        private float runSpeedMultiplier;

        public ChaseState(EnemyController _enemy) : base(_enemy)
        {
            en = _enemy;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Chase State!");
            baseEnemy.AnimancerComponent.Play(en.walkAnim);
            interval = Time.time + Random.Range(en.chaseToAttackTimeInterval.x, en.chaseToAttackTimeInterval.y);
            runSpeedMultiplier = Random.Range(1.6f, 1.8f);
        }

        public override void PhysicTick()
        {
            if (Mathf.Sign(baseEnemy.CurrentDirection) != Mathf.Sign(baseEnemy.PlayerDirection().x))
            {
                baseEnemy.Flip();
            }


            if (Time.time > interval)
            {
                if (Mathf.Abs(Vector2.Distance(baseEnemy.transform.position, baseEnemy.playerTransform.position)) < en.chaseToAttackTriggerDistance)
                {
                    if (baseEnemy.usePreAttack)
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

            switch (en.AggroStatus)
            {
                case EnemyAggroStatus.Semi:

                    if (baseEnemy.CheckMask(baseEnemy.borderMask) && !baseEnemy.Resetting)
                        baseEnemy.StopMove();
                    else
                    {
                        baseEnemy.Move(runSpeedMultiplier);
                    }
                    break;

                default:
                    baseEnemy.Move(runSpeedMultiplier);
                    break;
            }


        }

    }
}
