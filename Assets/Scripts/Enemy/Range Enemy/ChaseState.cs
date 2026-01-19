using UnityEngine;

public static partial class RangeType
{
    public class ChaseState : CharacterState
    {
        RangeTypeEnemy curr;
        float interval;

        public ChaseState(RangeTypeEnemy _enemy) : base(_enemy)
        {
            curr = _enemy;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Chase State!");
            baseEnemy.SetAnimatorState(baseEnemy.anim, "Enemy_Walk");
            //interval = Time.time + curr.nextProjectileTimer;
        }

        public override void Tick()
        {
            //curr.RotateTowards(baseEnemy.playerTransform.position);
            
            if (Mathf.Sign(baseEnemy.CurrentDirection) != Mathf.Sign(baseEnemy.PlayerDirection().x))
            {
                baseEnemy.Flip();
            }

            if (Time.time > interval)
            {
                baseEnemy.SetState(curr.attackState);
            }
        }

       
    }
}
