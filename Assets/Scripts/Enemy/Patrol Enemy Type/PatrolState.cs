using UnityEngine;

public static partial class PatrolType
{
    public class PatrolState : CharacterState
    {
        float interval = 2f;
        float resetting;
        PatrolTypeEnemy en;

        public PatrolState(PatrolTypeEnemy _enemy) : base(_enemy)
        {
            en = _enemy;
        }

        public override void EnterState()
        {
            Debug.Log("Patrol State!");
            baseEnemy.SetAnimatorState(baseEnemy.anim, "Enemy Default State");

            if (baseEnemy.Resetting)
                resetting = Time.time + 5f;
        
        }

        public override void Tick()
        {
            
            if (baseEnemy.Resetting)
                CheckResetting();
            else
                Patrolling();           
                

            if(baseEnemy.AggroStatus != EnemyAggroStatus.Calm)
            {
                if(Mathf.Abs(Vector2.Distance(baseEnemy.transform.position, baseEnemy.playerTransform.position)) < 10f)
                {
                    baseEnemy.SetState(baseEnemy.chaseState);
                }
            }

        }

        public override void PhysicTick()
        {
            baseEnemy.anim.SetFloat("EnemySpeed", Mathf.Clamp01(Mathf.Abs( baseEnemy.rb.velocity.x)));
        }


        private void Patrolling()
        {
            if (Time.time >= interval)
            {
                baseEnemy.Flip();
                interval = Time.time + Random.Range(5, 12);
            }

            baseEnemy.Move(0.7f);
        }

        private void CheckResetting()
        {
            baseEnemy.StopMove();
            if (Time.time > resetting)
            {
                baseEnemy.ResetPosition();
                interval = Time.time + 12;
                baseEnemy.Resetting = false;
            }   

        }

    }
}
