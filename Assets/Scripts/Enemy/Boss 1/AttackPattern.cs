
using UnityEngine;

public static partial class BossRaisin
{
    public class AttackPattern : EnemyBossState
    {

        RaisinBossController controller;
        float atkPatternTimer = 0f;

        public AttackPattern(RaisinBossController en)
        {
            controller = en;
        }

        public override void EnterState()
        {
            atkPatternTimer = Time.time + Random.Range(controller.neutralTimeRange.x, controller.neutralTimeRange.y);
        }

        public override void Tick()
        {
            if (Time.time > atkPatternTimer)
            {
                controller.SetState(controller.defaultState);
            }

            Debug.Log("Attack Pattern 1");
        }
    }
}
