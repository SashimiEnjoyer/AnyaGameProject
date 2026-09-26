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
            en.AnimancerComponent.Play(en.walkAnim);
        }

        public override void Tick()
        {
            
            if(en.AggroStatus != EnemyAggroStatus.Calm)
            {
                if(Mathf.Abs(en.transform.position.x - en.playerTransform.position.x) < en.idleToChaseTriggerDistance.x &&
                    Mathf.Abs(en.transform.position.y - en.playerTransform.position.y) < en.idleToChaseTriggerDistance.y)
                {
                    en.SetState(en.chaseState);
                }
            }

            //if (en.Resetting)
            //    CheckResetting();
            //else
                Patrolling();           

        }

        public override void PhysicTick()
        {
            en.AnimancerComponent.Play(Mathf.Abs(en.rb.linearVelocity.x) > 0? en.walkAnim : en.idleClip);
        }

        private void Patrolling()
        {
            if (Time.time >= interval)
            {
                en.Flip();
                interval = Time.time + Random.Range(en.patrolTimeRange.x, en.patrolTimeRange.y);
            }
            else
            {

                switch(en.AggroStatus)
                {
                    case EnemyAggroStatus.Semi:
                        
                        if (en.CheckMask(en.borderMask) && !en.Resetting)
                            en.StopMove();
                        else
                        {
                            en.Move(1f);
                        }
                        break;

                    default:
                        en.Move(1f);
                        break;
                }    
            }

        }

        //private void CheckResetting()
        //{
        //    en.StopMove();
        //    if (Time.time > resetting)
        //    {
        //        en.ResetPosition();
        //        interval = Time.time + 12;
        //        en.Resetting = false;
        //    }   

        //}

    }
}
