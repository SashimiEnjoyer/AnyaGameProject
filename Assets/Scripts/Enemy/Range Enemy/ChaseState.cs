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
            interval = Time.time + 5f;
        }

        public override void Tick()
        {
            RotateTowards(baseEnemy.playerTransform.position);
            
            if (Mathf.Sign(baseEnemy.CurrentDirection) != Mathf.Sign(baseEnemy.PlayerDirection().x))
            {
                baseEnemy.Flip();
            }

            if (Time.time > interval)
            {
                baseEnemy.SetState(curr.attackState);
            }
        }

        private void RotateTowards(Vector2 target)
        {
            Vector2 direction = (target - (Vector2)curr.projectilePos.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            curr.projectilePos.rotation = Quaternion.Euler(Vector3.forward * (angle));
        }
    }
}
