using UnityEngine;

public static partial class BossRaisin
{
    public class NeutralState : EnemyBossState
    {
        RaisinBossController controller;
        float neutralTiming = 0f;
        public NeutralState(RaisinBossController en) { controller = en; }

        public override void EnterState()
        {
            neutralTiming = Time.time + Random.Range(controller.neutralTimeRange.x, controller.neutralTimeRange.y);
        }

        public override void Tick()
        {
            if (Time.time > neutralTiming)
                controller.SetState(controller.attackPattern1);

            Debug.Log("Neutral State");
        }
    }
}
