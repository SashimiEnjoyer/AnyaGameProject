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
            en.AnimancerComponent.Play(en.walkAnim);
            interval = Time.time + Random.Range(en.chaseToAttackTimeInterval.x, en.chaseToAttackTimeInterval.y);
            runSpeedMultiplier = Random.Range(1.6f, 1.8f);
        }

        public override void PhysicTick()
        {
            if (Mathf.Sign(en.CurrentDirection) != Mathf.Sign(en.PlayerDirection().x))
            {
                en.Flip();
            }


            if (Time.time > interval)
            {
                if (Mathf.Abs(Vector2.Distance(en.transform.position, en.playerTransform.position)) < en.chaseToAttackTriggerDistance)
                {
                    if (en.usePreAttack)
                        en.SetState(en.preAttackState);
                    else
                        en.SetState(en.attackState);
                }

                else if (Mathf.Abs(en.transform.position.x - en.playerTransform.position.x) > 35f ||
                        Mathf.Abs(en.transform.position.y - en.playerTransform.position.y) > 7f)
                {
                    //en.transform.position = en.startingPoint.position;
                    en.SetState(en.defaultState);

                }
            }

            switch (en.AggroStatus)
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

    }
}
