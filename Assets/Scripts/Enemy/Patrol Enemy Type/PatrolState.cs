using UnityEngine;

public static partial class PatrolType
{
    public class PatrolState : CharacterState
    {
        float timer = 0f;
        float interval = 2f;

        public PatrolState(EnemyController enemy) : base(enemy)
        {
        }

        public override void EnterState()
        {
            enemy.SetAnimatorState(enemy.anim, "Enemy_Walk");
        }

        public override void Tick()
        {
            timer += Time.deltaTime;
            
            if (enemy.Resetting)
                CheckResetting();
            else
                Patrolling();

            if (enemy.CheckMask(enemy.borderMask) && !enemy.Resetting)
                enemy.StopMove();
            else
                enemy.Move(0.7f);

        }

        private void Patrolling()
        {
            if (timer >= interval)
            {
                enemy.Flip();
                interval = Random.Range(2, 6);
                timer = 0f;
            }
        }

        private void CheckResetting()
        {
            if (timer >= 5f)
            {
                enemy.ResetPosition();
                enemy.Resetting = false;
            }   

        }

    }
}
