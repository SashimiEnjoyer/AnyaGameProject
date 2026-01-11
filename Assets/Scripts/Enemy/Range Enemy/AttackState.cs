using Animancer;
using UnityEngine;

public static partial class RangeType
{
    [System.Serializable]
    public class AttackState : CharacterState
    {
        private GameObject proj;

        PatrolTypeEnemy en;
        AnimancerState state;

        public AttackState(PatrolTypeEnemy _enemy) : base(_enemy)
        {
            en = _enemy;
            en.onEnemyDoAttack += DoAttack;
        }

        public override void EnterState()
        {
            state = baseEnemy.AnimancerComponent.Play(en.attackClip);
            state.Events(this).OnEnd ??= OnEnd;

            if (proj != null)
            {
                proj.transform.parent = en.projectileOutputPos;
                proj.transform.eulerAngles = en.projectileOutputPos.eulerAngles;
                proj.transform.position = Vector3.zero;
            }
        }

        public override void PhysicTick()
        {
            
            if (Mathf.Sign(baseEnemy.CurrentDirection) != Mathf.Sign(baseEnemy.PlayerDirection().x))
            {
                baseEnemy.Flip();
            }

            RotateTowards(en.playerTransform.position);
        }

        void OnEnd()
        {
            baseEnemy.SetState(baseEnemy.chaseState);
        }

        private void DoAttack(bool state)
        {
            if (!state)
                return;

            if (proj == null)
                proj = Object.Instantiate(en.attackHitBox, en.projectileOutputPos);

            proj.SetActive(true);
            proj.transform.SetParent(null);
            proj.transform.position = en.projectileOutputPos.position;
            proj.GetComponent<ProjectileMove>()?.MoveProjectile(en.projectileSpeed, 4);
        }

        private void RotateTowards(Vector2 target)
        {
            Vector2 direction = (target - (Vector2)en.projectileOutputPos.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            en.projectileOutputPos.rotation = Quaternion.Euler(Vector3.forward * (angle));
        }

        public override void ExitState()
        {
            
            
        }
    }
}

