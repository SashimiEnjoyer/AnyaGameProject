using UnityEngine;

public static partial class PatrolType
{
    public class PatrolState : CharacterState
    {
        float interval = 2f;
        //float resetting;
        EnemyController en;

        public PatrolState(EnemyController _enemy) : base(_enemy)
        {
            en = _enemy;
        }

        public override void EnterState()
        {
            Debug.Log("Patrol State! " + en.gameObject.name);
            baseEnemy.AnimancerComponent.Play(en.walkAnim);
        }

        public override void Tick()
        {
            
            if(en.AggroStatus != EnemyAggroStatus.Calm)
            {
                if(Mathf.Abs(baseEnemy.transform.position.x - baseEnemy.playerTransform.position.x) < en.idleToChaseTriggerDistance.x &&
                    Mathf.Abs(baseEnemy.transform.position.y - baseEnemy.playerTransform.position.y) < en.idleToChaseTriggerDistance.y)
                {
                    baseEnemy.SetState(baseEnemy.chaseState);
                }
            }

            //if (baseEnemy.Resetting)
            //    CheckResetting();
            //else
                Patrolling();           

        }

        public override void PhysicTick()
        {
            baseEnemy.AnimancerComponent.Play(baseEnemy.rb.linearVelocity.x > 0? baseEnemy.walkAnim : baseEnemy.idleClip);
        }

        private void Patrolling()
        {
            if (Time.time >= interval)
            {
                baseEnemy.Flip();
                interval = Time.time + Random.Range(en.patrolTimeRange.x, en.patrolTimeRange.y);
            }
            else
            {

                switch(en.AggroStatus)
                {
                    case EnemyAggroStatus.Semi:
                        
                        if (baseEnemy.CheckMask(baseEnemy.borderMask) && !baseEnemy.Resetting)
                            baseEnemy.StopMove();
                        else
                        {
                            baseEnemy.Move(1f);
                        }
                        break;

                    default:
                        baseEnemy.Move(1f);
                        break;
                }    
            }

        }

        //private void CheckResetting()
        //{
        //    baseEnemy.StopMove();
        //    if (Time.time > resetting)
        //    {
        //        baseEnemy.ResetPosition();
        //        interval = Time.time + 12;
        //        baseEnemy.Resetting = false;
        //    }   

        //}

    }
}
