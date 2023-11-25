using UnityEngine;

public static partial class PatrolType
{
    public class ChaseState : CharacterState
    {
        PatrolTypeEnemy en;
        public ChaseState(PatrolTypeEnemy _enemy) : base(_enemy)
        {
            en = _enemy;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Chase State!");
            baseEnemy.SetAnimatorState(baseEnemy.anim, "Enemy_Walk");
        }

        public override void Tick()
        {

            if (baseEnemy.AggroStatus == EnemyAggroStatus.Semi)
            {
                if (baseEnemy.CheckMask(baseEnemy.borderMask) && !baseEnemy.Resetting)
                    baseEnemy.StopMove();
            }

            baseEnemy.Move(1f);

            if (Mathf.Sign(baseEnemy.CurrentDirection) != Mathf.Sign(baseEnemy.PlayerDirection().x))
            {
                baseEnemy.Flip();
            }
        }

        public override void PhysicTick()
        {
            if (Mathf.Abs(Vector2.Distance(baseEnemy.transform.position, baseEnemy.playerTransform.position)) < 3)
                baseEnemy.SetState(baseEnemy.attackState);


            if (Mathf.Abs(Vector2.Distance(baseEnemy.transform.position, baseEnemy.playerTransform.position)) > 35)
            {
                baseEnemy.transform.position = baseEnemy.startingPoint.position;
                baseEnemy.SetState(baseEnemy.defaultState);

            }
        }

        public override void ExitState()
        {
            baseEnemy.Resetting = true;
        }
    }
}
