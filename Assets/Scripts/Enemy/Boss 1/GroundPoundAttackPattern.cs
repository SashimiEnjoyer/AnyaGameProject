using Cysharp.Threading.Tasks;
using UnityEngine;

public static partial class BossRaisin
{
    public class GroundPoundAttackPattern : EnemyBossState
    {

        RaisinBossController controller;

        int repetition;

        public GroundPoundAttackPattern(RaisinBossController en)
        {
            controller = en;
        }

        public override void EnterState()
        {
            controller.StopMove();

            repetition = Random.Range(1, 4);

            _ = GroundPound();
        }

        public override void ExitState()
        {
            controller.StopMove();
            controller.rb.gravityScale = 1f;
        }

        private async UniTask GroundPound()
        {
            if(repetition <= 0)
            {
                controller.SetState(controller.defaultState);
                return;
            }

            controller.rb.gravityScale = 1;

            // 1. JUMP UP
            if (Mathf.Sign(controller.DirectionFacing) != Mathf.Sign(controller.PlayerDirection().x))
            {
                controller.Flip();
            }

            controller.rb.AddForce(new Vector2((controller.PlayerDirection().x < 0 ? -10f : 10f), 15f) , ForceMode2D.Impulse);

            // Wait until the boss reaches the peak of the jump
            await UniTask.WaitUntil(() => controller.rb.linearVelocity.y <= 0.1f);

            // 2. HOVER
            controller.rb.linearVelocity = Vector2.zero;
            controller.rb.gravityScale = 0; // Disable gravity to "float"

            await UniTask.WaitForSeconds(0.2f);

            // 3. SLAM DOWN
            controller.rb.gravityScale = 1 * 2;
            controller.rb.linearVelocity = new Vector2(0, -50 * 1.5f); // Optional extra punch

            await UniTask.WaitForSeconds(0.3f);
            controller.InstantiateGroundPoundEffect();

            await UniTask.WaitForSeconds(1f);

            repetition--;
            _ = GroundPound();
            
        }
    }
}
