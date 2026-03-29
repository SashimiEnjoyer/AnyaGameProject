using UnityEngine;

public static partial class BossRaisin
{
    public class DashAttackPattern : EnemyBossState
    {
        RaisinBossController controller;
        float currentPatternLiveTime = 0f;
        float holdAtk = 0f;

        public DashAttackPattern(RaisinBossController en)
        {
            controller = en;
        }

        public override void EnterState()
        {
            holdAtk = Time.time + 1f;
            currentPatternLiveTime = Time.time + 3f;

            controller.SetActiveHitbox(true);
            controller.PlayIdleAnim();

            if (Mathf.Sign(controller.DirectionFacing) != Mathf.Sign(controller.PlayerDirection().x))
            {
                controller.Flip();
            }
        }

        public override void Tick()
        {
            if (Time.time > currentPatternLiveTime)
            {
                controller.SetState(controller.defaultState);
            }
        }

        public override void ExitState()
        {
            controller.SetActiveHitbox(false);
        }

        public override void PhysicTick()
        {
            if(Time.time > holdAtk)
                controller.Move(3f);
            else
                controller.StopMove();
        }
    }
}
