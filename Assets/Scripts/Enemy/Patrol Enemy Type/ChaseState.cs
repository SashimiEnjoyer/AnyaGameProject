using UnityEngine;

public static partial class PatrolType
{
    public class ChaseState : CharacterState
    {
        public ChaseState(EnemyController enemy) : base(enemy)
        {
        }

        public override void EnterState()
        {
            enemy.SetAnimatorState(enemy.anim, "Enemy_Walk");
        }

        public override void Tick()
        {

            if (Mathf.Sign(enemy.CurrentDirection) != Mathf.Sign(enemy.PlayerDirection().x))
            {
                enemy.Flip();
            }

            enemy.Move(2f);

            if (Mathf.Abs(Vector2.Distance(enemy.transform.position, enemy.player.position)) < 3)
                enemy.SetState(enemy.attackState);

            if (Mathf.Abs(Vector2.Distance(enemy.transform.position, enemy.startingPoint.position)) < 15)
            {
                enemy.transform.position = enemy.startingPoint.position;
                enemy.SetState(enemy.patrolState);

            }
        }

        public override void ExitState()
        {
            enemy.Resetting = true;
        }
    }
}
