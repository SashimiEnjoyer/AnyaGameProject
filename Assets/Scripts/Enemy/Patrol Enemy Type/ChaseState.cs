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
            baseEnemy.SetAnimatorState(baseEnemy.anim, "Enemy_Walk");
            interval = Time.time + Random.Range(0.25f, 0.6f);
        }

        public override void Tick()
        {
            if (Time.time > interval)
            {
                if (Mathf.Abs(Vector2.Distance(baseEnemy.transform.position, baseEnemy.playerTransform.position)) < en.minAttackTriggerRange)
                    baseEnemy.SetState(baseEnemy.attackState);

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
                        baseEnemy.Move(2.3f);
                    }
                    break;

                default:
                    baseEnemy.Move(2.3f);
                    break;
            }
            
            if (Mathf.Sign(baseEnemy.CurrentDirection) != Mathf.Sign(baseEnemy.PlayerDirection().x))
            {
                baseEnemy.Flip();
            }

        }

    }
}
