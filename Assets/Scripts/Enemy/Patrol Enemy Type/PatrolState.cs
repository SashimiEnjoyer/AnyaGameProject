using UnityEngine;

public static partial class PatrolType
{
    public class PatrolState : CharacterState
    {
        float interval = 2f;
        //float resetting;
        PatrolTypeEnemy en;

        public PatrolState(PatrolTypeEnemy _enemy) : base(_enemy)
        {
            en = _enemy;
        }

        public override void EnterState()
        {
            Debug.Log("Patrol State! " + en.gameObject.name);
            baseEnemy.SetAnimatorState(baseEnemy.anim, "Enemy Default State");

            //if (baseEnemy.Resetting)
            //    resetting = Time.time + 5f;
        
        }

        public override void Tick()
        {
            
            if(en.AggroStatus != EnemyAggroStatus.Calm)
            {
                if(Mathf.Abs(baseEnemy.transform.position.x - baseEnemy.playerTransform.position.x) < en.aggroRadiusX &&
                    Mathf.Abs(baseEnemy.transform.position.y - baseEnemy.playerTransform.position.y) < en.aggroRadiusY)
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
            baseEnemy.anim.SetFloat("EnemySpeed", Mathf.Clamp01(Mathf.Abs( baseEnemy.rb.linearVelocity.x)));
        }


        private void Patrolling()
        {
            if (Time.time >= interval)
            {
                baseEnemy.Flip();
                interval = Time.time + Random.Range(en.minPatrolTime, en.maxPatrolTime);
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
