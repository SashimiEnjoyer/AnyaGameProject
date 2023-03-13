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

            if (enemy.CheckMask(enemy.borderMask) && !enemy.Resetting)
            {
                enemy.StopMove();
            }else
                enemy.Move(1f);

            if (Mathf.Abs(Vector2.Distance(enemy.transform.position, enemy.player.position)) < 3)
                enemy.SetState(enemy.attackState);
        }

        public override void ExitState()
        {
            enemy.Resetting = true;
        }
    }
}
