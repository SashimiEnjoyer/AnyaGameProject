using UnityEngine;

public static partial class BossRaisin
{
    public class RockRainAttackPattern : EnemyBossState
    {
        RaisinBossController controller;

        public RockRainAttackPattern(RaisinBossController en)
        {
            controller = en;
        }
    }
}
