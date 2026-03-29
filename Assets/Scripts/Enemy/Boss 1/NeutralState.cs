using UnityEngine;

public static partial class BossRaisin
{
    public class NeutralState : EnemyBossState
    {
        RaisinBossController controller;
        float moveStateTimer = 0f;
        float walkIdleInterval = 0f;
        public NeutralState(RaisinBossController en) { controller = en; }

        public override void EnterState()
        {
            moveStateTimer = Time.time + Random.Range(controller.neutralTimeRange.x, controller.neutralTimeRange.y);

            walkIdleInterval = Time.time + 1f;
        }

        public override void Tick()
        {
            if (Time.time > moveStateTimer) 
            {
                float randomness = Random.Range(0, 11);

                if(randomness > 5)
                    controller.SetState(controller.groundPoundAtkPattern);
                else
                    controller.SetState(controller.dashAtkPattern);
            }

            if (Mathf.Abs( controller.rb.linearVelocityX) > 0.1f)
            {
                controller.PlayWalkAnim();
            }
            else
            {
                controller.PlayIdleAnim();
            }
        }

        public override void PhysicTick()
        {
            if (Time.time >= walkIdleInterval)
            {
                controller.Flip();
                walkIdleInterval = Time.time + Random.Range(controller.neutralTimeRange.x - 1, controller.neutralTimeRange.y - 1);
            }
            else
            {
                controller.Move(1f);
            }
        }
    }
}
