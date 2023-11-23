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
            enemy.SetAnimatorState(enemy.anim, "Enemy Default State");

            if (enemy.Resetting)
                resetting = Time.time + 5f;
        
        }

        public override void Tick()
        {
            
            if (enemy.Resetting)
                CheckResetting();
            else
                Patrolling();           
                

            if(enemy.AggroStatus != EnemyAggroStatus.Calm)
            {
                if(Mathf.Abs(Vector2.Distance(enemy.transform.position, enemy.playerTransform.position)) < 10f)
                {
                    enemy.SetState(enemy.chaseState);
                }
            }

        }

        public override void PhysicTick()
        {
            enemy.anim.SetFloat("EnemySpeed", Mathf.Clamp01(Mathf.Abs( enemy.rb.velocity.x)));
        }


        private void Patrolling()
        {
            if (Time.time >= interval)
            {
                enemy.Flip();
                interval = Time.time + Random.Range(5, 12);
            }

            enemy.Move(0.7f);
        }

        private void CheckResetting()
        {
            enemy.StopMove();
            if (Time.time > resetting)
            {
                enemy.ResetPosition();
                interval = Time.time + 12;
                enemy.Resetting = false;
            }   

        }

    }
}
