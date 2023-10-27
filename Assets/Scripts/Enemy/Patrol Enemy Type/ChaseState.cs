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
            Debug.Log("Enter Chase State!");
            enemy.SetAnimatorState(enemy.anim, "Enemy_Walk");
        }

        public override void Tick()
        {

            if (enemy.AggroStatus == EnemyAggroStatus.Semi)
            {
                if (enemy.CheckMask(enemy.borderMask) && !enemy.Resetting)
                    enemy.StopMove();
            }

            enemy.Move(1f);

            if (Mathf.Sign(enemy.CurrentDirection) != Mathf.Sign(enemy.PlayerDirection().x))
            {
                enemy.Flip();
            }
        }

        public override void PhysicTick()
        {
            if (Mathf.Abs(Vector2.Distance(enemy.transform.position, enemy.playerTransform.position)) < 3)
                enemy.SetState(enemy.attackState);


            if (Mathf.Abs(Vector2.Distance(enemy.transform.position, enemy.playerTransform.position)) > 35)
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
